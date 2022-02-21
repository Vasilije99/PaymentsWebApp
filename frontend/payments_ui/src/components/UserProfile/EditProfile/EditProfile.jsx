import React, { useState, useEffect } from 'react'
import backend from '../../../api/backend';
import { useNavigate } from 'react-router';
import './editProfile.css'

const EditProfile = () => {
    const id = window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
    const [user, setUser] = useState({});
    const navigate = useNavigate();

    const fetchUser = async () => {
        const response = await backend.get(`/account/getUser/${id}`);

        setUser(response.data);
    }

    const updateUser = async () => {
        try {
            await backend.put(`/account/update/${id}`, user);

            window.confirm("User successfully updated");
            navigate(`/profile/${id}`);
        } catch (error) {
            alert(error);
        }
    }

    useEffect(() => {
        fetchUser();
    }, [])

    const handleSubmit = (e) => {
        e.preventDefault();

        updateUser();
    }

    return (
        <div className='userInfo edit'>
            <img src="../../../images/man.jpg" alt={user.name} />

            <form className='loginForm' onSubmit={(e) => handleSubmit(e)}>
                <label>
                    <i className="fa-solid fa-user" />
                    <input type="text" value={user.name} onChange={(e) => setUser(prevState => ({ ...prevState, name: e.target.value }))} />
                </label>
                <label>
                    <i className="fa-solid fa-user" />
                    <input type="text" value={user.lastname} onChange={(e) => setUser(prevState => ({ ...prevState, lastname: e.target.value }))} />
                </label>
                <label>
                    <i className="fa-solid fa-at" />
                    <input type="email" value={user.email} onChange={(e) => setUser(prevState => ({ ...prevState, email: e.target.value }))} />
                </label>
                <label>
                    <i className="fa-solid fa-phone" />
                    <input type="number" value={user.phoneNumber} onChange={(e) => setUser(prevState => ({ ...prevState, phoneNumber: Number(e.target.value) }))} />
                </label>

                <div className="buttons">
                    <button type="submit" className='loginButton'>Update</button>
                    <button className='loginButton' onClick={() => navigate(`/profile/${id}`)}>Go Back</button>
                </div>
            </form>
        </div>
    )
}

export default EditProfile