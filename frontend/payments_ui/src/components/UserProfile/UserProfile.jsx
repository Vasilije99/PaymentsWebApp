import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom';
import backend from '../../api/backend';
import './userProfile.css'

import VerifiedProfile from './VerifiedProfile/VerifiedProfile';
import CreditCard from '../CreditCard/CreditCard';

const UserProfile = () => {
    const id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
    const navigate = useNavigate();
    const [user, setUser] = useState({});
    const [verify, setVerify] = useState(true);

    const fetchUser = async () => {
        const response = await backend.get(`/account/getUser/${id}`);
        setUser(response.data);
        setVerify(response.data.isVerified);
    }

    const verifyAccount = async () => {
        await backend.put(`/account/verify/${id}`);
        setVerify(true);
    }

    useEffect(() => {
        fetchUser();
    }, [verify])

    
    const RenderPage = () => verify ? (
        <VerifiedProfile user={user} id={id}/>
    ) : (
        <div className="creditCard">
            <h3>You need to verify your account</h3>

            <CreditCard 
                btnText={'Verify'} 
                desc={'You need to pay $1 to verificate account. Name field is optional'} 
                transaction={verifyAccount}
            />
            <button className='verifyButton' onClick={() => navigate('/')}>Go Back </button>
        </div>
    )

    return (
        <RenderPage />
    )
}

export default UserProfile