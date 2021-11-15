import React, { Component } from 'react';
import './LoginForm.css';
import { create } from 'ipfs-http-client';
const { createAlchemyWeb3 } = require("@alch/alchemy-web3");
const { Accounts } = require("web3-eth-accounts");

const PROJECT_ID = process.env.REACT_APP_INFURA_PROJECT_ID;
const PROJECT_SECRET = process.env.REACT_APP_INFURA_PROJECT_SECRET;
const INFURA_ENDPOINT = process.env.REACT_APP_INFURA_ENDPOINT;
const API_URL = process.env.REACT_APP_ALCHEMY_API_URL;
const PUBLIC_KEY = process.env.REACT_APP_PUBLIC_KEY;
const PRIVATE_KEY = process.env.REACT_APP_PRIVATE_KEY;
const auth = 'Basic' + Buffer.from(PROJECT_ID + ':' + PROJECT_SECRET).toString('base64');

const web3 = createAlchemyWeb3(API_URL);
const nftContractFile = require("../artifacts/AutoNet.json");
const nftContractAddress = "0xAe93ecd9E8196aBC3091Eb42807d4a8119823fCF";
const nftContract = new web3.eth.Contract(nftContractFile.abi, nftContractAddress);

const ipfsClient = create({
    url: INFURA_ENDPOINT,
    headers: {
        authorization: auth
    }
});


export class Mint extends Component {
    static displayName = Mint.name;

    constructor(props) {
        super(props);
        this.state = {
            customerName: "", customerEmail: "", walletAddress: "0x", nftFile: {}, uploaded: false,
            mintSuccess: false, mintFail: false, displayMessage: false, message: "Nothing is happening", creatingWallet: false,
            newWalletAddress: "", newWalletPrivateKey: "", info: '', displayInfo: false
        };
        this.updateCustomerName = this.updateCustomerName.bind(this);
        this.updateCustomerEmail = this.updateCustomerEmail.bind(this);
        this.updateWalletAddress = this.updateWalletAddress.bind(this);
        this.updateNftFile = this.updateNftFile.bind(this);
        this.validateForm = this.validateForm.bind(this);
        this.submitForm = this.submitForm.bind(this);
        this.createNewWallet = this.createNewWallet.bind(this);
    }


    componentDidMount() {
        
    }


    /**********HELPER FUNCTIONS************/
    validateEmail(email) {
        const regExp = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regExp.test(String(email).toLowerCase());;
    }
    validateWallet(wallet) {
        const regExp = /^0x[a-fA-F0-9]{40}$/;
        return regExp.test(wallet);;
    }
    validateName(name) {
        return name.length > 0;;
    }
    validateFile() {
        //TODO: Add functionality for only accepting certain extensions
        return this.state.uploaded;
    } 

    updateCustomerName(event) {
        this.setState({
            customerName: event.target.value
        });
    }

    updateCustomerEmail(event) {
        this.setState({
            customerEmail: event.target.value
        });
    }

    updateWalletAddress(event) {
        var updatedString = event.target.value;
        if (updatedString.length < 2) {
            event.target.value = "0x" + updatedString;
        } else if (updatedString[0] != '0' || updatedString[1] != 'x') {
            event.target.value = "0x" + updatedString;
        }
        this.setState({
            walletAddress: event.target.value
        });
    }

    createNewWallet(event) {
        var createWallet = !this.state.creatingWallet;
        this.setState({
            creatingWallet: createWallet
        });
        if (createWallet) {
            const newWallet = web3.eth.accounts.create();
            this.setState({
                newWalletAddress: newWallet.address,
                newWalletPrivateKey: newWallet.privateKey,
                walletAddress: newWallet.address
            });

        }
    }

    updateNftFile(event) {
        if (event.target.files) {
            this.setState({
                nftFile: event.target.files[0],
                uploaded: true
            });
        } else {
            this.setState({
                uploaded: false
            });
        }        
    }

    validateForm() {
        return (this.validateEmail(this.state.customerEmail) && this.validateWallet(this.state.walletAddress) && this.validateName(this.state.customerName) && this.validateFile());
    }

    async mintNFT(metadataURL) {
        const nonce = await web3.eth.getTransactionCount(PUBLIC_KEY, "latest");
        const trans = {
            from: PUBLIC_KEY,
            to: nftContractAddress,
            nonce: nonce,
            gas: 500000,
            data: nftContract.methods.mintNFT(this.state.walletAddress, metadataURL).encodeABI()
        };
        const signPromise = web3.eth.accounts.signTransaction(trans, PRIVATE_KEY);
        return signPromise.then((signedTrans) => {
            return web3.eth.sendSignedTransaction(signedTrans.rawTransaction, function (err, hash) {
                console.log(hash);
                if (err) {
                    return "0";
                } else {
                    return hash;
                }
            });
        }).catch((err) => {
            return "0";
        });

    }


    async submitForm(event) {
       event.preventDefault();
        try {
            this.setState({
                mintFail: false,
                mintSuccess: false,
                displayMessage: true,
                message: "Minting! Step 1 of 3..."
            });
            const addedFile =  await ipfsClient.add(this.state.nftFile);
            const fileUrl = 'https://ipfs.infura.io/ipfs/' + addedFile.path;

            const metaDataDoc = JSON.stringify({
                description: "Minted by AutoNet",
                image: "https://ipfs.infura.io/ipfs/" + addedFile.path,
                name: this.state.customerName,
                attributes: []
            });

            this.setState({
                message: "Minting! Step 2 of 3..."
            });

            const addedJson = await ipfsClient.add(metaDataDoc);
            const metadataUrl = 'https://ipfs.infura.io/ipfs/' + addedJson.path;

            this.setState({
                message: "Minting! Step 3 of 3..."
            });

            const transDetails = await this.mintNFT(metadataUrl);
            if (transDetails != "0") {
                console.log(transDetails);
                console.log(transDetails.transactionHash);
                console.log(transDetails.gasUsed);
                this.setState({
                    mintSuccess: true,
                    mintFail: false,
                    displayMessage: false,
                    customerName: "",
                    customerEmail: "",
                    walletAddress: "",
                    nftFile: {}
                });
                var walletInfo = "Your New Wallet Address is:\n" + this.state.newWalletAddress +
                    "\nAnd Your Private Key Is:\n" + this.state.newWalletPrivateKey +
                    "\nPRINT THIS PAGE AND KEEP THIS INFO SECURE!";
                this.setState({
                    displayInfo: true,
                    info: walletInfo
                });
                document.getElementById("nftFile").value = null;
            } else {
                this.setState({
                    mintFail: true,
                    mintSuccess: false,
                    displayMessage: false,
                    displayInfo: false
                });
            }       
        } catch (error) {
            console.log(error);
            this.setState({
                mintFail: true,
                mintSuccess: false,
                displayMessage: false,
                displayInfo: false
            });
        }            
    }


    render() {
        return (
            <div class="col-md-9 login-form-1">
                <h3>Mint</h3>
                <form onSubmit={this.submitForm} >
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Customer Name" name="customername" value={ this.state.customerName } autoFocus onChange={this.updateCustomerName} />
                    </div>
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Customer Email" name="email" value={this.state.customerEmail } onChange={this.updateCustomerEmail} />
                    </div>
                    <div class="form-group">
                        <label class="max-width">
                            <input type="checkbox" class="form-control" id="create-checkbox" value="Create New Wallet" name="newwallet" checked={this.state.creatingWallet} onChange={this.createNewWallet} />
                            Create New Wallet
                        </label>
                    </div>
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Customer Wallet Address" id="addressInput" name="address" value={this.state.walletAddress} disabled={ this.state.creatingWallet} onChange={this.updateWalletAddress} />
                    </div>
                    <div class="form-group">
                        <input type="file" class="form-control cursor-pointer" placeholder="" name="nftfile " id="nftFile" onChange={this.updateNftFile} />
                    </div>
                    <div class="form-group">
                        <input type="submit" class="btnSubmit" value="Mint" disabled={!this.validateForm() && !this.state.displayMessage} />
                    </div>
                    {this.state.mintSuccess && (
                        <div class="form-group">
                            <div class="alert alert-success" role="alert">
                                Your file was successfully minted!
                            </div>
                        </div>
                    )}
                    {this.state.mintFail && (
                        <div class="form-group">
                            <div class="alert alert-danger" role="alert">
                                Something went wrong.
                            </div>
                        </div>
                    )}
                    {this.state.displayMessage && (
                        <div class="alert alert-info" role="alert">
                            { this.state.message }
                        </div>
                    )}
                    {this.state.displayInfo && (
                        <div class="alert alert-info" role="alert">
                            {this.state.info}
                        </div>
                    )}
                </form>
            </div>
        );
    }
}