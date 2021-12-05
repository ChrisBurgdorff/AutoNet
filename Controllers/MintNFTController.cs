using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFTDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MintNFTController : ControllerBase
    {
        private readonly AutoNetDemoDBContext _context;
        private readonly string connString;

        public MintNFTController(AutoNetDemoDBContext context)
        {
            _context = context;
            connString = _context.Database.GetDbConnection().ConnectionString;
        }


        // POST api/<MintNFTController>
        [HttpPost]
        public async Task<ActionResult<int>> PostMint([FromBody] MintNftTransaction value)
        {
            int nftId = -1;
            if (value == null)
            {
                return Ok(nftId); 
            }

            using (SqlConnection con = new SqlConnection(connString))
            {
                try
                {
                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();
                }
                catch
                {
                    //TODO: Error HAndling
                }
                using (SqlCommand cmd = new SqlCommand("spMintNft", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CustomerName", System.Data.SqlDbType.NVarChar)).Value = value.CustomerName;
                    cmd.Parameters.Add(new SqlParameter("@CustomerEmail", System.Data.SqlDbType.NVarChar)).Value = value.CustomerEmail;
                    cmd.Parameters.Add(new SqlParameter("@WalletAddress", System.Data.SqlDbType.NVarChar)).Value = value.WalletAddress;
                    cmd.Parameters.Add(new SqlParameter("@UserID", System.Data.SqlDbType.Int)).Value = value.UserID;
                    cmd.Parameters.Add(new SqlParameter("@IpfsUrl", System.Data.SqlDbType.NVarChar)).Value = value.IpfsUrl;
                    SqlParameter outputParameter = new SqlParameter();
                    outputParameter.ParameterName = "@NftID";
                    outputParameter.SqlDbType = System.Data.SqlDbType.Int;
                    outputParameter.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);
                    try
                    {
                        int cmdResult = cmd.ExecuteNonQuery();
                        nftId = (int)outputParameter.Value;
                    }
                    catch
                    {
                        //TODO: ERROR HANDLING
                    }
                }
            }
            return Ok(nftId);
        }

    }
}
