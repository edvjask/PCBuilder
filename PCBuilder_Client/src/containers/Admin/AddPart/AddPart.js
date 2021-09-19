import React, {Component} from 'react';
import axios from 'axios';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';

class AddPart extends Component {

    state = {
        
        submitStatus: null,
        name : null,
        img : null,
        specsInputs : [],
        productType : null,
        errors : null
    }

    typeChangeHandler = (e) => {
        axios.get('https://localhost:5001/specification/' + e)
            .then(response => {
                //console.log(response);
                let formSpecs = [];
                for (let item of response.data.data){
                    const obj = {
                        [item.id] : {
                            name : item.name,
                            inputs : [" "],
                            multiple : item.multiple
                        }
                    }
                    formSpecs.push(obj);
                }
                this.setState({ specsInputs: formSpecs});
                this.setState({productType : response.data.data[0].productType});
            }).catch(er => {
                console.log(er);
            })
    }

    specInputAdded = (ind) => {
        
        let modifiedSpecInputs = [...this.state.specsInputs];
        Object.values(modifiedSpecInputs[ind])[0].inputs.push(" ");
        this.setState({specsInputs : modifiedSpecInputs});
    }

    specInputRemoved = (ind) => {
        let modifiedSpecInputs = [...this.state.specsInputs];
        Object.values(modifiedSpecInputs[ind])[0].inputs.splice(-1,1);
        this.setState({specsInputs : modifiedSpecInputs});
    }

    nameChangedHandler = (e) => {
        this.setState({name : e});
    }

    specChanged = (specIndex, ind, value) => {
        let specInputsChanged = [...this.state.specsInputs];
        Object.values(specInputsChanged[specIndex])[0].inputs[ind] = value;
        this.setState({specsInputs: specInputsChanged});
    }

    handleUpload = (e) => {
        this.setState({img: e.target.files[0]});

    }

    submitPressedHandler = (e) => {
        e.preventDefault();
        const postData = {
            name : this.state.name,
            producttype: this.state.productType
        }

        axios.post('https://localhost:5001/products/add', postData,{ 
        headers : {
            'Authorization' : `Bearer ${localStorage.getItem('token')}`
        }}).then(response => {

            if (!response.data.success) {
                this.setState({errors: response.data.message});
            }

             //console.log(response.data);

             let formData = new FormData();

             formData.append('file', this.state.img);
     
             axios.post("https://localhost:5001/imageupload/product/" + response.data.data.id, formData, {
                 headers: {
                     "Content-Type": "undefined",
                     'Authorization' : `Bearer ${localStorage.getItem('token')}`
                 }
             }).then(resp => {
                 //console.log(resp.data);
             }).catch(er => {
                 console.log(this.state.img);
                 console.log(er.response.data);
             });    


             let promises = [];
            for (let spec of this.state.specsInputs){

                const values = Object.values(spec)[0];
                const key = Object.keys(spec)[0];
                for (let inputValue of values.inputs)
                {
                    
                    const post = {
                        productid : response.data.data.id,
                        specificationid : parseInt(key),
                        value : inputValue
                    }

                    
                    promises.push(
                    axios.post('https://localhost:5001/productspecification', post, {
                        headers : {
                            'Authorization' : `Bearer ${localStorage.getItem('token')}`
                        }
                    }).then(resp => { 
                        
                    }).catch(er => {
                        //console.log(er.response.data);
                    }));
                }

                Promise.all(promises)
                    .then(resp => {
                        this.props.history.push("/admin/addproduct/success");
                    });
            }

        }).catch(er => {
            console.log(er);
        })
    }

    

    render(){

        let productSpecs = null;
        if (this.state.specsInputs){
            productSpecs = this.state.specsInputs.map((spec, index) => {
                // console.log(spec);
                // console.log(Object.keys(spec)[0]);
                
                const key = Object.keys(spec)[0];
                const value = Object.values(spec)[0];
                
                return (<FormGroup key={index}>
                    <Label for={key}>{value.name}</Label>
                    {value.inputs.map((input,ind) => {
                        return (<Input 
                        key={ind}
                        type="text" 
                        name={value.name + ind} 
                        id={key}
                        onChange={(e) => this.specChanged(index,ind,e.target.value)} />);
                    })}
                    { value.multiple ?
                    <Button color="success" onClick={() => this.specInputAdded(index)}>Add</Button> : null
                    }
                    {
                        value.inputs.length > 1 ? 
                        <Button color="danger" onClick={() => this.specInputRemoved(index)}>Remove</Button> :
                        null
                    }
                </FormGroup>);
            });
        }

        return (
            <div style={{maxWidth: '500px', margin: '10px auto'}}>
            <h2>Add New Part</h2>
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
                    <Label for="name">Product Name</Label>
                    <Input 
                        type="text" 
                        name="name" 
                        id="name" 
                        placeholder="Enter product name"
                        onChange={(e) => this.nameChangedHandler(e.target.value)} />
                </FormGroup>
                <FormGroup>
                    <Label for="exampleFile">Select product photo</Label>
                    <Input style={{display: 'block', margin: '5px auto'}} type="file" name="file" id="exampleFile" onChange={this.handleUpload} />
                    <FormText color="muted">
                        Selected photo must be in .jpg .jpeg or .png format
                    </FormText>
                </FormGroup>

                <legend>Product Specifications</legend>
                {productSpecs}
                <Button onClick={(e) => this.submitPressedHandler(e)}>Add Product</Button>
                <p style={{color: 'red'}}>{this.state.errors}</p>
            </Form>
            </div>
        );
    }
}

export default AddPart