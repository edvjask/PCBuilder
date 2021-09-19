import axios from 'axios';

export const CHANGE_COMPONENT = 'CHANGE_COMPONENT';
export const SELECT_PART = 'SELECT_PART';
export const DELETE_SELECTION = 'DELETE_SELECTION';
export const UPDATE_COMPATIBILITY_PROBLEMS = 'UPDATE_COMPATIBILITY_PROBLEMS';
export const UPDATE_PARTS = 'UPDATE_PARTS';
export const DELETE_PARTS = 'DELETE_PARTS';


export const saveSelectedPart = (id, partTypeId) => {
    return {
        type: SELECT_PART,
        partType: partTypeId,
        partId : id
    };
}

export const updateCompatibility = (problemsList) => {
    return {
        type: UPDATE_COMPATIBILITY_PROBLEMS,
        problems: problemsList
    }
}

export const deleteSelection = (compId, partList) => {
    return dispatch => {
        dispatch({type: DELETE_SELECTION, id: compId})
        dispatch(checkCompatibilityAsync(partList))
    }
}

export const updateParts = (parts) => {
    return {
        type: UPDATE_PARTS,
        partList : parts
    }
}

export const getPartListAsync = (amountValue) => {
    return function (dispatch) {
        const postData = {
            amount : amountValue
        }
        axios.post('https://localhost:5001/products/getcomplete', postData)
            .then(response => {
                //console.log(response.data.data);
                dispatch(updateParts(response.data.data));
                 
            })
            .catch(error => {
                console.log(error);
            });
    }
}

export const checkCompatibilityAsync = (partList) => {
    return function (dispatch) {
        let partListDto = {
            "processorid" : partList['1'].data ? partList['1'].data.id : null,
            "cpucoolerid" : partList['2'].data ? partList['2'].data.id : null,
            "motherboardid" : partList['3'].data ? partList['3'].data.id : null,
            "memoryid" : partList['4'].data ? partList['4'].data.id : null,
            "storageid" : partList['5'].data ? partList['5'].data.id : null,
            "gpuid" : partList['6'].data ? partList['6'].data.id : null,
            "caseid" : partList['7'].data ? partList['7'].data.id : null,
            "psuid" : partList['8'].data ? partList['8'].data.id : null
    
        };
        axios.post('https://localhost:5001/compatibility/', partListDto)
            .then(response => {
                //console.log(response.data.data);
                dispatch(updateCompatibility(response.data.data.problems));
                 
            })
            .catch(error => {
                console.log(error);
            });
    }
    
}

export const selectPart = (id, partTypeId, partList) => {
    return function (dispatch) {
        axios.get('https://localhost:5001/products/' + id)
            .then(response => {
                //console.log(response.data);
                dispatch(saveSelectedPart(response.data.data, partTypeId));
                dispatch(checkCompatibilityAsync(partList));
                
            })
            .catch(error => {
                console.log(error);
            });
    };

}