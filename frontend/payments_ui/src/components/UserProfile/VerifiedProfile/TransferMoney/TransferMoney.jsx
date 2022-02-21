import React, { useState } from 'react'
import backend from '../../../../api/backend';
import CreditCard from '../../../CreditCard/CreditCard'
import './transferMoney.css'

const TransferMoney = ({ user }) => {
    const [amount, setAmount] = useState(0);
    const [email, setEmail] = useState('');

    const onlineAccTransfer = async (e) => {
        e.preventDefault();

        try {
            await backend.put(`/account/onlineTransfer?email=${email}&amount=${amount}&id=${user}`);
            window.confirm("Transaction was successfully executed");
            document.location.reload(true);
        } catch (error) {
            alert(error);
        }
    }

    const transaction =  async (ccAmount) => {
        try {
            await backend.put(`/account/bankTransfer?amount=${ccAmount}&id=${user}`);
            window.confirm("Transaction was successfully executed");
            document.location.reload(true);
        } catch(error) {
            alert(error);
        }
    }

    return (
        <div className='transferMoneyDiv'>
            <h2>Initiate a new transaction to another user</h2>
            <hr />
            <div className="twoWayTransfer">
                <div className="leftSide">
                    <h3>Transfer to on-line account:</h3>
                    <form onSubmit={(e) => onlineAccTransfer(e)}>
                        <label>
                            <i className="fa-solid fa-at" />
                            <input type="email" placeholder='Who are you sending to?' onChange={(e) => setEmail(e.target.value)} />
                        </label>
                        <label>
                            <i className="fa-solid fa-circle-dollar-to-slot" />
                            <input type="number" placeholder='How much do you send?' onChange={(e) => setAmount(parseFloat(e.target.value))} />
                        </label>
                        <button type='submit'>
                            SEND
                        </button>
                    </form>
                </div>
                <div className="rightSide">
                    <h3>Transfer to bank account:</h3>
                    <CreditCard btnText={'SEND'} transaction={transaction}/>
                </div>
            </div>
        </div>
    )
}

export default TransferMoney