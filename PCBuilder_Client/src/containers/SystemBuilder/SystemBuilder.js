import React, { Component } from 'react';
import { Button } from 'reactstrap';
import {connect} from 'react-redux';
import * as actionTypes from '../../store/actions/actions';

import './SystemBuilder.css';

import SystemBuilderTable from './SystemBuilderTable/SystemBuilderTable';
import Modal from './Modal/Modal';

class SystemBuilder extends Component {

    state = {
        isModalOpen : false
    }

    toggleModalHandler = () => {
        const toggle = !this.state.isModalOpen;
        this.setState({isModalOpen : toggle})
    }

    startNewHandler = () => {
        if (window.confirm("Are you sure you want to delete all parts?")){
            this.props.onDeleteParts();
        }
    }

    render() {
        return (
            <div>
                <h2>Select Your Parts</h2>
                <Button
                    size="lg"
                    style={{margin: '5px 20px'}} 
                    color="secondary" 
                    onClick={this.toggleModalHandler}>
                    Export         
                </Button>
                <Button 
                    size="lg"
                    color="danger" 
                    onClick={this.startNewHandler}>
                    Start New         
                </Button>
                <Modal 
                    isShowing={this.state.isModalOpen}
                    toggle={this.toggleModalHandler}
                    parts={this.props.parts} />
                <SystemBuilderTable />
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        parts: state.parts
    };
}

const mapDispatchToAction = dispatch => {
    return {
        onDeleteParts: () => dispatch({type: actionTypes.DELETE_PARTS})
    }
}



export default connect(mapStateToProps, mapDispatchToAction)(SystemBuilder);