import React, { useState } from 'react'
import { useNavigate } from 'react-router'
import backend from '../../../api/backend';
import CreditCard from '../../CreditCard/CreditCard';
import TransferMoney from './TransferMoney/TransferMoney';
import './verifiedProfile.css'

const VerifiedProfile = ({ user, id }) => {
    const navigate = useNavigate();
    const [dialog, setDialog] = useState(false);

    const transaction = async (amount) => {
        await backend.put(`/account/updateBudget/${id}?amount=${amount}`);
        document.location.reload(true);
    }

    return (
        <>
            <div className="links">
                <a href={`/transactions/${id}`}>Transactions History</a>
                <a href="/">Log Out</a>
            </div>
            
            <div className="mainContent">
                <div className='verifiedProfileDiv'>
                    <div className='userInfo'>
                        <img src="../../../images/man.jpg" alt={user.name} />
                        <button className='profileButton' onClick={() => navigate(`/editUser/${id}`)}>
                            Edit Info
                        </button>

                        <div> Name: <span>{user.name}</span> </div>
                        <div> Lastname: <span>{user.lastname}</span> </div>
                        <div> Email: <span>{user.email}</span> </div>
                        <div> Phone: <span>{user.phoneNumber}</span> </div>
                    </div>

                    <div className="userBudget">
                        <h3>Budget: {user.budget}$</h3>
                        <button className='profileButton' onClick={() => setDialog(true)}>
                            Update your budget
                        </button>
                    </div>
                </div>

                <TransferMoney user={id} />
            </div>

            {
                dialog === true ?
                    <div className='dialog'>
                        <CreditCard btnText={'Pay'} transaction={transaction} />
                        <button onClick={() => setDialog(false)} >Close</button>
                    </div> : null
            }
        </>
    )
}

export default VerifiedProfile