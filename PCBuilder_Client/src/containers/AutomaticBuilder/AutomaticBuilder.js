import React, {Component} from 'react';
import {connect} from 'react-redux';
import * as actionTypes from '../../store/actions/actions';
import {Button, Form, FormGroup, Input, Label} from 'reactstrap';

class AutomaticBuilder extends Component {

    state = {
        amount : 0
    }

    handleSubmit = (e) => {
        e.preventDefault();
        const a = window.confirm("This will overwrite any current part list. Proceed?");
        if (a) {
            this.props.onPartListGet(this.state.amount);
            this.props.history.push('/systembuilder');
        }
    }

    handleInputChange = (e) => {
        this.setState({amount : e.target.value});
    }

    render(){
        return (
            <div 
            style={{maxWidth: '500px', margin: '50px auto'}}>
                <Form onSubmit={this.handleSubmit}>
                    <FormGroup>
                        <Label for="amountValue">Enter Your Budget:</Label>
                        <Input 
                            type="text" 
                            name="amount" 
                            id="amountValue"
                            onChange={this.handleInputChange}
                            placeholder="Amount in €" />
                        
                    </FormGroup>
                    <Button>Build!</Button>
                </Form>
            </div>
        );
    }
}



const mapDispatchToProps = dispatch => {
    return {
        onPartListGet: (amount) => dispatch(actionTypes.getPartListAsync(amount))
    }
}


export default connect(null,mapDispatchToProps)(AutomaticBuilder);