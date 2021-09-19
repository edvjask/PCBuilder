import React, {Component, Fragment} from 'react';
import axios from 'axios';
import './ComponentInfo.css';
import {connect} from 'react-redux';
import * as actionTypes from '../../../store/actions/actions';
import placeholderImg from '../../../placeholder-image.png';

class ComponentInfo extends Component {

    state = {
        component: null,
        adverts: null
    }

    componentDidMount() {
       
        axios.get('https://localhost:5001/products/full/' + this.props.match.params.id)
            .then(response => {
                this.setState({component: response.data.data});
            })
            .catch(error => {

            });
        axios.get('https://localhost:5001/advert/all/' + this.props.match.params.id)
        .then(response => {
            this.setState({adverts: response.data.data});
        })
        .catch(error => {

        });
    }

    render() {

        let component = null;

        if (this.state.component && this.state.adverts){
            //console.log(this.state.component.specifications);
            const imgPath = this.state.component.imagePath ? this.state.component.imagePath : placeholderImg;
            component = (
                <Fragment>
                    <div className="product">   
                        <h2>{this.state.component.name}</h2>
                        <hr></hr>
                        
                        <img src={imgPath } alt="" />
                    </div>
                    <button onClick={() => {
                        this.props.onItemAdd(this.state.component.id, this.state.component.productType, this.props.parts);
                        this.props.history.push('/systembuilder') }}>Add
                    </button>
                    <button onClick={() => {
                        this.props.history.goBack() }}>Back
                    </button>
                    <div className="float-container">
                    <div className="float-child specs">
                        <h4 style={{textAlign: 'center'}}>Specifications</h4>
                        <ul>
                        {this.state.component.specifications.map(spec => {
                            return (
                            <Fragment key={spec.name}>
                            <li 
                                
                                style={{fontWeight:'bold'}} >{spec.name}</li>
                            <ul>
                            {spec.value.map((specValue,ind) => {
                                return <li key={ind}>{specValue}</li>
                            })}
                            </ul>
                            </Fragment>
                            );
                        })}
                        </ul>
                    </div>
                    <div className="float-child adverts">
                        <h4>Adverts</h4>
                        <table className="table">
                            <thead>
                                <tr>
                                <th scope="col">Seller Name</th>
                                <th scope="col">Description</th>
                                <th scope="col">Price</th>
                                <th scope="col">Location</th>
                                <th scope="col">Phone</th>
                                </tr>
                            </thead>
                        <tbody>
                            {this.state.adverts.map(ad => {
                                return(
                                <tr key={ad.id}>
                                    <td>{ad.seller.name}</td>
                                    <td>{ad.description.slice(0,13)}...</td>
                                    <td>{ad.price}</td>
                                    <td>{ad.seller.location}</td>
                                    <td>{ad.seller.phone}</td>
                                </tr>
                                );
                            })}
                        </tbody>
                        </table>
                    </div>
                    </div>
                </Fragment>
            );
        }


        return (
            <div className="container">
                {component}
            </div>
        );
    }


}

const mapStateToProps = state => {
    return {
        parts: state.parts
    };
}

const mapDispatchToProps = dispatch => {
    return {
        onItemAdd: (id, partTypeId, partList) => dispatch(actionTypes.selectPart(id, partTypeId, partList))
    }
}



export default connect(mapStateToProps,mapDispatchToProps)(ComponentInfo);