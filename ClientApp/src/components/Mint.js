import React, { Component } from 'react';
import './LoginForm.css';
import { create } from 'ipfs-http-client';

const PROJECT_ID = process.env.REACT_APP_INFURA_PROJECT_ID;
const PROJECT_SECRET = process.env.REACT_APP_INFURA_PROJECT_SECRET;
const INFURA_ENDPOINT = process.env.REACT_APP_INFURA_ENDPOINT;
const auth = 'Basic' + Buffer.from(PROJECT_ID + ':' + PROJECT_SECRET).toString('base64');

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
            customerEmail: "", walletAddress: "", nftFile: {} };
        this.updateCustomerEmail = this.updateCustomerEmail.bind(this);
        this.updateWalletAddress = this.updateWalletAddress.bind(this);
        this.updateNftFile = this.updateNftFile.bind(this);
        this.validateForm = this.validateForm.bind(this);
        this.submitForm = this.submitForm.bind(this);
    }

    updateCustomerEmail(event) {
        this.setState({
            customerEmail: event.target.value
        });
    }

    updateWalletAddress(event) {
        this.setState({
            walletAddress: event.target.value
        });
    }

    updateNftFile(event) {
        this.setState({
            nftFile: event.target.files[0]
        });
    }

    validateForm() {
        return true;
    }


    async submitForm(event) {
        alert("Your fucking email is " + this.state.email + " and your fucking file is " + this.state.nftFile.name);
        event.preventDefault();
        try {
            const addedFile =  await ipfsClient.add(this.state.nftFile);
            const fileUrl = 'https://ipfs.infura.io/ipfs/' + addedFile.path;
            alert(fileUrl);
        } catch (error) {
            console.log(error);
        }
            
    }


    render() {
        return (
            <div class="col-md-9 login-form-1">
                <h3>Mint</h3>
                <form onSubmit={this.submitForm} >
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Customer Email *" name="email" autoFocus onChange={this.changeEmail} />
                    </div>
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Customer Wallet Address" name="address " onChange={this.updateWalletAddress} />
                    </div>
                    <div class="form-group">
                        <input type="file" class="form-control" placeholder="" name="nftfile " onChange={this.updateNftFile} />
                    </div>
                    <div class="form-group">
                        <input type="submit" class="btnSubmit" value="Mint" disabled={!this.validateForm()} />
                    </div>
                </form>
            </div>
        );
    }
}