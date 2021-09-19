import React from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Label, Input } from 'reactstrap';
import './Modal.css';

const ModalExample = (props) => {
  


  let formatedList = "PCBuilder Part List\n";
  formatedList += "-----------------------------------\n";
  formatedList += "Part Name                     Price\n";


  //console.log(formatedList);

  let totalPrice = 0;
  Object.values(props.parts).map(part => {
      
      if (part.data){
        let price = 0;
        
        if (part.data.lowestPrice !== -1){
            price = part.data.lowestPrice;
            totalPrice += price;
        }
        formatedList += part.name + '\n';
        formatedList += part.data.name + "       " + price.toFixed(2) + '€\n';
        formatedList += "-----------------------------------\n";
      }
      return (true);
  });

  formatedList += "Total Price: " + totalPrice.toFixed(2) + "€";

  
  

  return (
    <div>
      <Modal 
        contentClassName="custom-modal-style"
        isOpen={props.isShowing} 
        toggle={props.toggle} 
        centered={true} >
        <ModalHeader>Export Your Parts List</ModalHeader>
        <ModalBody>
            <Label>
                <Input checked type="radio" name="radio1" readOnly />{' '}
                Text
            </Label>
            <Label>
                <Input type="radio" name="radio1" />{' '}
                HTML
            </Label>
            <textarea
                style={{width: '100%', height: '100%'}}
                value={formatedList}
                readOnly >
                
            </textarea>
        </ModalBody>
        <ModalFooter>
          <Button color="secondary" onClick={props.toggle}>Close</Button>
        </ModalFooter>
      </Modal>
    </div>
  );
}

export default ModalExample;