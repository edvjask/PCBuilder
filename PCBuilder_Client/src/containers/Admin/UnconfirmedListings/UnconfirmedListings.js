import React, {Component} from 'react';
import axios from 'axios';
import {Table, Button} from 'reactstrap';

class Unconfirmed extends Component {
    state = {
        adverts : null
    }

    componentDidMount () {
        axios.get('https://localhost:5001/advert/unconfirmed',{ 
        headers : {
            'Authorization' : `Bearer ${localStorage.getItem('token')}`
        }}).then(response => {
            this.setState({adverts: response.data.data});
        }).catch(er => {
            console.log(er);
        })
    }

    confirmed = (id) => {
        axios.put('https://localhost:5001/advert/confirm/' + id,{}, {
            headers : {
                'Authorization' : `Bearer ${localStorage.getItem('token')}`
            }
        }).then(resp => {
            if (resp.data.success){
                this.props.history.go(0);
            }
        }).catch(er => {
            //console.log(localStorage.getItem('token'));
        })
    }
    deleted = (id) => {
        axios.delete('https://localhost:5001/advert/delete/' + id, {
            headers : {
                'Authorization' : `Bearer ${localStorage.getItem('token')}`
            }
        }).then(resp => {
            if (resp.data.success){
                this.props.history.go(0);
            }
        }).catch(er => {
            //console.log(er.response.data);
        })
    }


    render() {

        let advs = null;
        if (this.state.adverts){
            advs = this.state.adverts.map(adv => {
                return (
                <tr key={adv.id}>
                    <td>{adv.seller.name}</td>
                    <td>{adv.createdOn}</td>
                    <td>{adv.productName}</td>
                    <td>{adv.price}€</td>
                    <td>{adv.description.slice(0,15)}...</td>
                    <td><Button color="success" onClick={() => this.confirmed(adv.id)}>Confirm</Button> </td>
                    <td><Button color="danger" onClick={() => this.deleted(adv.id)} >Delete</Button> </td>
                </tr>);
                
            });
        }

        return (
            <div style={{maxWidth: '70%', margin: '10px auto'}}>
                <h2>Unconfirmed Listings</h2>
                <Table responsive>
                    <thead>
                        <tr>
                            <th>Seller</th>
                            <th>Created On</th>
                            <th>Product Name</th>
                            <th>Price</th>
                            <th>Description</th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {advs}
                    </tbody>
                </Table>
            </div>
        );
    }
}

export default Unconfirmed;