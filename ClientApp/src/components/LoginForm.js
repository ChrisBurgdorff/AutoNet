import React, { Component } from 'react';
import './LoginForm.css';
import client from '../httpClient';
import axios from 'axios';

export class LoginForm extends Component {

    constructor(props) {
        super(props);
        this.state = { email: "", password: "" };
        this.changeEmail = this.changeEmail.bind(this);
        this.changePassword = this.changePassword.bind(this);
        this.submitLoginForm = this.submitLoginForm.bind(this);
    }

    changeEmail(event) {
        this.setState({
            email: event.target.value
        });
    }

    changePassword(event) {
        this.setState({
            password: event.target.value
        });
    }

    validateLoginForm() {
        return this.state.email.length > 0 && this.state.password.length > 0;
    }

    submitLoginForm(event) {
        //alert("Your fucking email is " + this.state.email + " and your fucking password is " + this.state.password);
        event.preventDefault();
        let formData = {
            email: this.state.email,
            password: this.state.password
        };
        axios.post('api/registration/login', formData).then((res) => {
            if (res.data.hasOwnProperty('message')) {
                //Login failed
                //TODO: Do something with message
            } else {
                localStorage.setItem('user', JSON.stringify(res.data));
                client.setTokenOnLogin();
            }            
        }).catch((res) => {
            console.log("Login Failed");
        });
    }

    render() {
        return (
            <div class="col-md-6 login-form-1">
                <h3>Login</h3>
                <form onSubmit={this.submitLoginForm} >
                    <div class="form-group">
                        <input type="text" class="form-control" placeholder="Your Email *" name="email" autoFocus onChange={this.changeEmail} />
                    </div>
                    <div class="form-group">
                        <input type="password" class="form-control" placeholder="Your Password *" name="password " onChange={this.changePassword} />
                    </div>
                    <div class="form-group">
                        <input type="submit" class="btnSubmit" value="Submit" disabled={!this.validateLoginForm()} />
                    </div>
                    <div class="form-group">
                        <a href="#" class="ForgetPwd">Forget Password?</a>
                    </div>
                </form>
            </div>
        );
    }
}

export default LoginForm;
