
import React from 'react';
import { NavLink } from 'react-router-dom';
import './NavbarLogo.css';
import {Button} from 'reactstrap';

const navbaruser = (props) => {

    const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('role');
        localStorage.removeItem('username');
        props.logout();
    }

    return (
    <div>
        
        <h1 style={{textAlign: 'left', margin: '10px 40px'}}>PCBuilder</h1>
        <div style={{maxWidth: '60%', margin: '10px 0px 10px 30%', display: 'inline-flex'}}>
            <p>Welcome back, {props.username}!</p>
            <Button onClick={logout}>Log Out</Button>
        </div>
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
        <div className="container-fluid">
            
            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup" aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
            <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
            <div className="navbar-nav">
                <NavLink
                    to="/systembuilder"
                    className="nav-link"
                    activeStyle={{
                        fontWeight: 'bold'
                    }}>System Builder</NavLink>
                <NavLink
                    to="/autobuilder"
                    exact
                    className="nav-link"
                    activeStyle={{
                        fontWeight: 'bold'
                    }}>Automatic Builder</NavLink>
                    <NavLink
                    to="/createlisting"
                    className="nav-link"
                    activeStyle={{
                        fontWeight: 'bold'
                    }}>Create Listing</NavLink>
                <NavLink
                    to="/userlistings"
                    className="nav-link"
                    activeStyle={{
                        fontWeight: 'bold'
                    }}>Your Listings</NavLink>
                
            </div>
            </div>
        </div>
        </nav>
    </div>
    );
}

export default navbaruser;