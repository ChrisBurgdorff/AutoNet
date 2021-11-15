import React, { Component } from 'react';
import LoginForm from './LoginForm.js';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <><div>
            <h1>Welcome To AutoNet!</h1>
            <p>Click <a href="/Mint">Mint</a> to Mint an NFT</p>
        </div><div class="row justify-content-md-center">
                <LoginForm />
            </div></>
    );
  }
}
