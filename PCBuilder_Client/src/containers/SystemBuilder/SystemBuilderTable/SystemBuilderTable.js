import React, {Component} from 'react';
import {withRouter} from 'react-router-dom';
import BuilderRow from './BuilderRow';
import {connect} from 'react-redux';
import * as actionTypes from '../../../store/actions/actions';

class SystemBuilderTable extends Component {

    state = {
        
        
    }



    buttonHandler = (id) => {

        this.props.history.push({pathname: this.props.match.url + '/' + this.props.parts[id].name});
        this.props.onChangeChoosable(id);
    }

    render() {

        //count total price
        let totalPrice = 0;

        Object.values(this.props.parts).map(part => {
            //only get price for selected
            if (part.data){
                if (part.data.lowestPrice !== -1){
                    totalPrice += part.data.lowestPrice;
                }
            }
            return true;
        });

        let partList = Object.entries(this.props.parts)
            .map(part => {
            //console.log(part[0]);    
            return <BuilderRow
                        key={part[0]}
                        name={part[1]['name']}
                        data={part[1]['data']}
                        clicked={() => this.buttonHandler(part[0])} 
                        deleted={() => this.props.onDeleteChosen(part[0], this.props.parts)}/>;
        });

        let problems = null;
        if (this.props.compatibilityProblems){
            //console.log(this.props.compatibilityProblems);
            problems = this.props.compatibilityProblems.compatibilityProblems
            .map((problem, index) => {
                return <p key={index}>{problem}</p>
            });
        }
        


        return (
            <div className="container">
                <table className="table">
                <thead>
                    <tr>
                    <th scope="col">Component</th>
                    <th scope="col">Part name</th>
                    <th scope="col">Price</th>
                    <th scope="col">Seller</th>
                    </tr>
                </thead>
                <tbody>
                    {partList}
                    <tr>
                        <td><strong>Total Price:</strong></td>
                        <td></td>
                        <td><strong>{totalPrice.toFixed(2) + "€"}</strong></td>
                        <td></td>
                    </tr>
                </tbody>
                </table>
                <div style={{textAlign: 'left'}}>
                    <h4 >Compatibility issues:</h4>
                    {problems}
                    
                </div>
            </div>
        );
    }
}

const mapStateToProps = state => {
    return {
        parts: state.parts,
        compatibilityProblems : state.compatibilityProblems
    };
}

const mapDispatchToAction = dispatch => {
    return {
        onChangeChoosable: (id) => dispatch({type: actionTypes.CHANGE_COMPONENT, value: id}),
        onDeleteChosen: (partTypeId, partList) => dispatch(actionTypes.deleteSelection(partTypeId, partList))

    }
}



export default withRouter(connect(mapStateToProps,mapDispatchToAction)(SystemBuilderTable));