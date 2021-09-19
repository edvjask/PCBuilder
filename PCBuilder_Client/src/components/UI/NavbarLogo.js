
import React from 'react';
import { NavLink } from 'react-router-dom';
import './NavbarLogo.css';

const navbarlogo = () => (
    <div>
        
        <h1 style={{textAlign: 'left', margin: '10px 40px'}}>PCBuilder</h1>
        <div style={{maxWidth: '60%', margin: '10px 0 10px 30%'}}>
            <NavLink
                to="/auth"
                className="top-nav"
                activeStyle={{
                    fontWeight: 'bold'
                }}>Login</NavLink>
            <NavLink
                to="/register"
                className="top-nav"
                activeStyle={{
                    fontWeight: 'bold'
                }}>Register</NavLink>
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
                
            </div>
            </div>
        </div>
        </nav>
    </div>
);

export default navbarlogo;