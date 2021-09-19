import React, {Component} from 'react';
import axios from 'axios';
import { Button, Form, FormGroup, Label, Input, CustomInput } from 'reactstrap';

class CreateListing extends Component {

    state = {
        products : null,
        description : "",
        price: 0,
        used: true,
        productId: null,
        serverError : null
    }

    typeChangeHandler = (value) => {
        axios.get('https://localhost:5001/products/all/' + value)
            .then(response => {
                if (response.data.success){
                    this.setState({products: response.data.data});
                }
            })
            .catch(er => {

            });
    }

    formInputHandler = (type, value) => {
        switch (type){
            case "desc":
                this.setState({description: value});
                break;
            case "price":
                this.setState({price: value});
                break;
            case "used":
                this.setState({used: value});
                break;
            case "product":
                this.setState({productId: value});
                break;
            default:
                break;   
        }
    }

    addListingHandler = (e) => {
        e.preventDefault();

        const postData = {
            price : parseFloat(this.state.price),
            description : this.state.description,
            used : this.state.used === 'true',
            productId : parseInt(this.state.productId)
        }
        //console.log(postData);
        
        axios.post('https://localhost:5001/advert/add', postData, {
            headers : {
                'Authorization' : `Bearer ${localStorage.getItem('token')}`
            }
        }).then(response => {
            if (!response.data.success){
                this.setState({serverError : response.data.message});
            }
            else {
                this.props.history.push('/userlistings');
            }
        }).catch(er => {
            console.log(er.response.data);
        })

    }

    render() {
        let productOptions = [<option key="" value="">Please select product</option>];
        if (this.state.products){
            productOptions.push (this.state.products.map(p => {
                return <option key={p.id} value={p.id}>{p.name}</option>
            }))
        }


        return (
            <div className="container" style={{maxWidth: '50%'}}>
                <h1>Create Listing</h1>
                <Form>
                <FormGroup>
                    <Label for="typeSelect">Select Component Type</Label>
                    <Input 
                        type="select" 
                        name="select" 
                        id="typeSelect"
                        onChange={(e) => this.typeChangeHandler(e.target.value)} >
                            <option value="">Please select type</option>
                            <option value="1">CPU</option>
                            <option value="2">CPU Cooler</option>
                            <option value="3">Motherboard</option>
                            <option value="4">Memory</option>
                            <option value="5">Storage</option>
                            <option value="6">Video Card</option>
                            <option value="7">Case</option>
                            <option value="8">Power Supply</option>
                    </Input>
                 </FormGroup>
                 <FormGroup>
                    <Label for="modelSelect">Select Product</Label>
                    <Input 
                        type="select" 
                        name="select" 
                        id="modelSelect"
                        onChange={(e) => this.formInputHandler('product', e.target.value)} >
                        {productOptions}
                    </Input>
                 </FormGroup>
                 <FormGroup>
                    <Label for="desc">Description</Label>
                    <Input 
                        type="textarea" 
                        name="description" 
                        id="desc"
                        placeholder="Add product description"
                        onChange={(e) => this.formInputHandler('desc', e.target.value)} >
                    </Input>
                 </FormGroup>
                 <FormGroup>
                    <Label for="exampleCheckbox">Item Used?</Label>
                    <div>
                    <CustomInput 
                        type="radio" 
                        id="exampleCustomRadio" 
                        name="customRadio"
                        value={true} 
                        label="Yes"
                        onChange={(e) => this.formInputHandler('used', e.target.value)} />
                    <CustomInput 
                        type="radio" 
                        id="exampleCustomRadio2" 
                        name="customRadio" 
                        value={false} 
                        label="No"
                        onChange={(e) => this.formInputHandler('used', e.target.value)} />
                    </div>
                </FormGroup>
                 <FormGroup>
                    <Label for="price">Price</Label>
                    <Input 
                        type="text" 
                        name="price" 
                        id="price"
                        placeholder="Price in €"
                        onChange={(e) => this.formInputHandler('price', e.target.value)} >
                    </Input>
                 </FormGroup>
                 <Button color="success" onClick={(e) => this.addListingHandler(e)}>Add</Button>
                </Form>
                <p style={{color: 'red'}}>{this.state.serverError}</p>
            </div>
            
        );
    }
}


export default CreateListing;