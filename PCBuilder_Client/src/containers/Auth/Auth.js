import React, {Component} from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import axios from 'axios';
import {withRouter} from 'react-router-dom';


class Auth extends Component {

    state = {
        email : null,
        password: null,
        serverErrors: null
    }

    emailChangeHandler = (e) => {
        this.setState({email: e.target.value});
    }

    passChangeHandler = (e) => {
        this.setState({password: e.target.value});
    }

    loginHandler = () => {

        let postData = {
            email : this.state.email,
            password : this.state.password
        }

        axios.post('https://localhost:5001/auth/login', postData)
            .then(response => {
                if (response.data.success){

                    localStorage.setItem('token', response.data.data.token);
                    localStorage.setItem('username', response.data.data.name);
                    localStorage.setItem('role', response.data.data.role);
                    const user = {
                        token : localStorage.getItem('token'),
                        name : localStorage.getItem('username'),
                        role : localStorage.getItem('role')
                    }
                    this.props.login(user);
                    this.props.history.replace('/');
                }
            })
            .catch(er => {
                //console.log(er);
                if (!er.response.data.success){
                    this.setState({serverErrors: er.response.data.message});
                }
            });
    }

    render () {
        //console.log(this.props.history);
        return (
            <div style={{maxWidth: '500px', margin: '20px auto'}}>
                <h2>Log Into Your Account</h2>
                <Form>
                    <FormGroup>
                        <Label for="exampleEmail">Email</Label>
                        <Input
                            required 
                            type="email" 
                            name="email" 
                            id="exampleEmail" 
                            placeholder="Enter email"
                            onChange={this.emailChangeHandler} />
                    </FormGroup>
                    <FormGroup>
                        <Label for="examplePassword">Password</Label>
                        <Input 
                            type="password" 
                            name="password" 
                            id="examplePassword" 
                            placeholder="Enter password"
                            onChange={this.passChangeHandler} />
                    </FormGroup>
                    <Button onClick={this.loginHandler}>Submit</Button>
                </Form>
                <p style={{color: 'red'}}>{this.state.serverErrors}</p>
            </div>
        );
    }
}

export default withRouter(Auth);