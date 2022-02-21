import React from 'react'
import './app.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Login from './components/Login/Login'
import Register from './components/Register/Register'
import EditProfile from './components/UserProfile/EditProfile/EditProfile';
import UserProfile from './components/UserProfile/UserProfile';
import Transactions from './components/UserProfile/VerifiedProfile/TransactionsHistory/Transactions';

const App = () => {
    return (
        <BrowserRouter>
            <div>
                <Routes>
                    <Route path='/' element={
                        <>
                            <h2 className='main header'>Welcome to payment website</h2>
                            <Login />
                        </>
                    }></Route>
                    <Route path='/register' element={
                        <Register />
                    }></Route>
                    <Route path='/profile/:id' element={
                        <UserProfile />
                    }></Route>
                    <Route path='/editUser/:id' element={
                        <EditProfile />
                    }></Route>
                    <Route path='/transactions/:id' element={
                        <Transactions />
                    }></Route>
                </Routes>
            </div>
        </BrowserRouter>
    )
}

export default App
