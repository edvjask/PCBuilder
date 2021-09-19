import { BrowserRouter } from 'react-router-dom';
import './App.css';
import {Route, Redirect,  Switch} from 'react-router-dom';
import SystemBuilder from './containers/SystemBuilder/SystemBuilder';
import NavbarLogo from './components/UI/NavbarLogo';
import NavbarUser from './components/UI/NavBarUser';
import ComponentChoose from './containers/SystemBuilder/ComponentChoose/ComponentChoose';
import ComponentInfo from './containers/SystemBuilder/ComponentInfo/ComponentInfo';
import AutomaticBuilder from './containers/AutomaticBuilder/AutomaticBuilder';
import Auth from './containers/Auth/Auth';
import CreateListing from './containers/CreateListing/CreateListing';
import React, { Component } from 'react';
import UserListings from './containers/UserListings/UserListings';
import NavBarAdmin from './components/UI/NavBarAdmin';
import AddPart from './containers/Admin/AddPart/AddPart';
import Unconfirmed from './containers/Admin/UnconfirmedListings/UnconfirmedListings';
import AddPartSuccess from './containers/AddPartSuccess/AddPartSuccess';
import Register from './containers/Auth/Register';


class App extends Component {


  state = {
    currentUser : {
      token : localStorage.getItem('token'),
      name : localStorage.getItem('username'),
      role : localStorage.getItem('role')
    }
  }


  shouldComponentUpdate = (nextProps, nextState) => {
    console.log("shouldComponentUpdate App.js");
    return this.state.currentUser.token !== nextState.currentUser.token;
  }
  
  logoutHandler = () => {
    const deleted = {
      token : null,
      name : null,
      role : null
    }
    this.setState({currentUser : deleted});
    
  }

  loginHandler = (user) => {
    this.setState({currentUser: user});
  }

  render() {

    let navMenu = <NavbarLogo />;
    if (this.state.currentUser){
      if (this.state.currentUser.role === 'seller'){
        navMenu = <NavbarUser username={this.state.currentUser.name} logout={this.logoutHandler} />
      }
      if (this.state.currentUser.role === 'admin'){
        navMenu = <NavBarAdmin username={this.state.currentUser.name} logout={this.logoutHandler} />
      }
    }
    if (!this.state.currentUser.token){
        console.log("Should redirect");
        
    }

    return (
      <BrowserRouter>
      <div className="App">

        {navMenu}
        {this.state.currentUser.token ? null : <Redirect to="/" exact />}
        <Switch>
          <Route path="/systembuilder" exact component={SystemBuilder} />
          <Route path="/systembuilder/:select" exact component={ComponentChoose} />
          <Route path="/products/:id" exact component={ComponentInfo} />
          <Route path="/autobuilder" component={AutomaticBuilder} />
          <Route path="/createlisting" component={CreateListing} />
          <Route path="/userlistings" component={UserListings} />
          <Route path="/admin/addproduct" exact component={AddPart} />
          <Route path="/admin/addproduct/success" exact component={AddPartSuccess} />
          <Route path="/admin/unconfirmed" exact component={Unconfirmed} />
          <Route path="/register" exact component={Register} />
          <Route path="/register/success" exact render={() => <p style={{margin: '100px auto'}}>Registration was successful, you can now login!</p>} />
          <Route path="/auth" exact render={() => <Auth login={(user) => this.loginHandler(user)}/>} />
          <Route path='/' render={() => <h1>Welcome to PCBuilder! Select what you want to do</h1>} />
        </Switch>
  
      </div>
      </BrowserRouter>
    );
  }
  
}

export default App;
