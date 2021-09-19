import {Link} from 'react-router-dom';
import './BuilderRow.css';
import {Button} from 'reactstrap';
import placeholderImg from '../../../placeholder-image.png';


function builderRow(props) {
    if (props.data){

        let sellerRow = props.data.phone === '-' ? 'No seller found' : props.data.phone + ',' + props.data.location;
        const imgPath = props.data.imagePath ? props.data.imagePath : placeholderImg;
        return (
            <tr>
                <td>{props.name}</td>
                <td>
                    <img src={imgPath} alt="" style={{width: '50px', height: '50px', margin: '0 10px'}} />
                    <Link 
                       to={"/products/" + props.data.id}
                       className="product-link" >
                        {props.data['name']}
                    </Link>
                    
                </td>
                <td>{props.data['lowestPrice'] === -1 ? '-' : props.data['lowestPrice'] + '€'}</td>
                <td>{sellerRow}</td>
                <td><Button color="primary" size="sm" onClick={props.deleted}>X</Button></td>
            </tr>
        );
    }
    else {
        return (
            <tr>
                <td>{props.name}</td>
                <td colSpan={3}><Button color="primary" size="sm" onClick={props.clicked}>Choose</Button></td>
                
            </tr>
        );
    }
}

export default builderRow;