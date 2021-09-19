import React, {Component} from 'react';
import axios from 'axios';
import {Table, Button} from 'reactstrap';

class UserListings extends Component {

    state = {
        listings : null
    }

    componentDidMount() {
        axios.get('https://localhost:5001/advert/user', {
            headers : {
                'Authorization' : `Bearer ${localStorage.getItem('token')}`
            }
        }).then(response => {
            //console.log(response.data);
            this.setState({listings : response.data.data});
        }).catch(er => {
            //console.log(er);
        });
    }

    render() {

        let listings = null;
        if (this.state.listings){
            listings = this.state.listings.map(l => {
                return (<tr key={l.id}>
                    <td>{l.productName}</td>
                    <td>{l.description.slice(0,15)}...</td>
                    <td>{l.createdOn} </td>
                    <td>{l.lastEditedOn} </td>
                    <td>{l.confirmed ? "Yes" : "No"}</td>
                    <td>{l.price}€ </td>
                    <td><Button>Edit</Button></td>
                    <td><Button color="danger">X</Button></td>
                </tr>);
            })
        }

        return (
            <div className="container" style={{maxWidth: '80%'}}>
                <h2>{localStorage.getItem('username')}'s Listings</h2>
                <Table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Description</th>
                            <th>Created On</th>
                            <th>Last Edited On</th>
                            <th>Confirmed</th>
                            <th>Price</th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {listings}
                    </tbody>
                </Table>
            </div>
        );
    }
}

export default UserListings;