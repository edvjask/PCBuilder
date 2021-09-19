import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import {connect} from 'react-redux';
import axios from 'axios';
import * as actionTypes from '../../../store/actions/actions';
import placeholderImg from '../../../placeholder-image.png';
import {Button} from 'reactstrap'

import '../SystemBuilderTable/BuilderRow.css';

class ComponentChoose extends Component {


    state = {
        components: null
    }

    componentDidMount () {

        const choosingId = this.props.choosing;

        axios.post('https://localhost:5001/products/' + this.props.parts[choosingId].apiLink, {})
            .then(response => {
                //console.log(response.data);
                const comps = response.data.data;
                this.setState({components: comps});
            })
            .catch(error => {
                console.log(error);
            });
    }

    

    render() {

        let compTable = null;

        if (this.state.components){
            compTable = <table className="table">
            <thead>
                <tr>
                <th scope="col">Part name</th>
                {this.state.components[0].specifications.map((spec, ind) => {
                   return (<th key={ind} scope="col">{spec.name}</th>)
                })}
                <th scope="col">Price</th>
                <th></th>
                </tr>
            </thead>
            <tbody>
                {this.state.components.map((comp, ind) => {
                    const imgPath = comp.imagePath ? comp.imagePath : placeholderImg;
                    return (<tr key={ind}>
                    <td>
                        <img src={imgPath} alt="" style={{width: '50px', height: '50px', margin: '0 10px'}} />
                        <Link
                            to={"/products/" + comp.id} 
                            className="product-link">
                            {comp.name}
                        </Link>
                    </td>
                    {comp.specifications.map((spec, ind) => <td key={ind}>{spec.value}</td>)}
                    <td>{comp.lowestPrice === -1 ? '-' : comp.lowestPrice + '€'}</td>
                    <td><Button onClick={() => {
                        this.props.onItemAdd(comp.id, this.props.choosing, this.props.parts);
                        this.props.history.push('/systembuilder') }}>Add</Button></td>
                    </tr>)
                })}
                
            </tbody>
            </table>
        }

        return (
            <div className="container">
                <h2>Select A {this.props.match.params.select}</h2>
                <Button onClick={this.props.history.goBack}>Go back</Button>
                {compTable}
                
                {/* <p>{this.props.choosing}</p> */}
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        parts: state.parts,
        choosing: state.selectedComponent
    };
}

const mapDispatchToProps = dispatch => {
    return {
        onItemAdd: (id, partTypeId, partList) => dispatch(actionTypes.selectPart(id, partTypeId, partList))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ComponentChoose);