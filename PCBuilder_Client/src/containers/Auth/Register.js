import React, {Component} from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import axios from 'axios';

class Register extends Component {

    state = {
        name : null,
        phone : null,
        email : null,
        password : null,
        location : null,
        passConfirm : null,
        serverErrors : null

    }

    inputTypeHandler = (type, e) => {
        switch(type) {
            case "user" :
                this.setState({name : e.target.value});
                break;
            case "email" :
                this.setState({email : e.target.value});
                break;
            case "phone" :
                this.setState({phone : e.target.value});
                break;
            case "loc" :
                this.setState({location : e.target.value});
                break;
            case "pass" :
                this.setState({password : e.target.value});
                break;
            case "passConfirm" :
                this.setState({passConfirm : e.target.value});
                break;
            default :
                break;

        }
    }

    registerHandler = () => {
        if (this.state.passConfirm !== this.state.password) {
            this.setState({serverErrors: "Passwords must match"});
            return;
        }
        else {
            const postData = {
                name : this.state.name,
                phone : this.state.phone,
                email : this.state.email,
                location : this.state.location,
                password : this.state.password
            }
    
            axios.post('https://localhost:5001/auth/register', postData)
                .then(response => {
                    if (response.data.success){
    
                        //console.log(response.data);
                        this.props.history.push('/register/success');
                    }
                })
                .catch(er => {
                    //console.log(er.response.data);
                    if (!er.response.data.success){
                        this.setState({serverErrors: er.response.data.message});
                    }
                });
        }
        

    }

    render() {
        return (
            <div style={{maxWidth: '500px', margin: '20px auto'}}>
                <h2>Create New Account</h2>
                <Form>
                    <FormGroup>
                        <Label for="user">Username</Label>
                        <Input
                            type="text" 
                            name="username" 
                            id="user" 
                            placeholder="Choose a Username"
                            onChange={(e) => this.inputTypeHandler("user", e)} />
                    </FormGroup>
                    <FormGroup>
                        <Label for="email">Email</Label>
                        <Input 
                            type="email" 
                            name="emailName" 
                            id="email" 
                            placeholder="Your Email Address"
                            onChange={(e) => this.inputTypeHandler("email", e)} />
                    </FormGroup>
                    <FormGroup>
                        <Label for="phone">Phone</Label>
                        <Input 
                            type="text" 
                            name="phoneAr" 
                            id="phone" 
                            placeholder="Enter Your Phone Number"
                            onChange={(e) => this.inputTypeHandler("phone", e)} />
                    </FormGroup>
                    <FormGroup>
                        <Label for="location">Location</Label>
                        <Input 
                            type="text" 
                            name="locAre" 
                            id="location" 
                            placeholder="Enter Your City/Town/Area"
                            onChange={(e) => this.inputTypeHandler("loc", e)} />
                    </FormGroup>
                    <FormGroup>
                        <Label for="examplePassword">Password</Label>
                        <Input 
                            type="password" 
                            name="password" 
                            id="examplePassword" 
                            placeholder="Enter password"
                            onChange={(e) => this.inputTypeHandler("pass", e)} />
                    </FormGroup>
                    <FormGroup>
                        <Label for="examplePassword2">Confirm Password</Label>
                        <Input 
                            type="password" 
                            name="password2" 
                            id="examplePassword2" 
                            placeholder="Enter password again"
                            onChange={(e) => this.inputTypeHandler("passConfirm", e)} />
                    </FormGroup>
                    <Button onClick={this.registerHandler}>Submit</Button>
                </Form>
                <p style={{color: 'red'}}>{this.state.serverErrors}</p>
                
            </div>
        );
    }
}

export default Register;