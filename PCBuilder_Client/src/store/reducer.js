//import update from 'immutability-helper';
import * as actionTypes from './actions/actions';

const initialState = {
    parts : {
        1 : {name: 'CPU', data: null, apiLink: 'compatible_cpu'},
        2 : {name: 'CPU Cooler', data: null, apiLink: 'compatible_cpucooler'},
        3 : {name: 'Motherboard', data: null, apiLink: 'compatible_mobo'},
        4 : {name: 'Memory', data: null, apiLink: 'compatible_ram'},
        5 : {name: 'Storage', data: null, apiLink: 'compatible_storage'},
        6 : {name: 'Video Card', data: null, apiLink: 'compatible_gpu'},
        7 : {name: 'Case', data: null, apiLink: 'compatible_case'},
        8 : {name: 'Power Supply', data: null, apiLink: 'compatible_psu'}
    },
    compatibilityProblems : null,
    selectedComponent: null,
    
}

const reducer = (state = initialState, action) => {

    switch(action.type){
        case actionTypes.CHANGE_COMPONENT: 
            return {
                ...state,
                selectedComponent: action.value
            }
        case actionTypes.SELECT_PART:
            const partsUpdated = {...state.parts};
            partsUpdated[action.partType].data = action.partId;
            return {
                ...state,
                parts: partsUpdated
            }

        case actionTypes.DELETE_SELECTION:
            const partsAfterDeletion = {...state.parts};
            partsAfterDeletion[action.id].data = null;
            return {
                ...state,
                parts: partsAfterDeletion
            }
        case actionTypes.UPDATE_COMPATIBILITY_PROBLEMS:
            const stateAfterComp = {...state.compatibilityProblems}
            stateAfterComp.compatibilityProblems = action.problems;
            return {
                ...state,
                compatibilityProblems: stateAfterComp
            }
        case actionTypes.UPDATE_PARTS:
            let partsCopy = {...state.parts}
            for (let part of action.partList){
                //console.log(part, partsCopy);
                partsCopy[part.productType].data = part;
            }
            return {
                ...state,
                parts : partsCopy
            }
        case actionTypes.DELETE_PARTS:
            let newParts = {...state.parts};
            Object.values(newParts).forEach(p => {
                p.data = null;
            });
            return {
                ...state,
                parts : newParts,
                compatibilityProblems : null
            }
        default:
            ;
    }


    return state;
}

export default reducer;