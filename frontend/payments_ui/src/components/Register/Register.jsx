import React, {useState} from 'react'
import backend from '../../api/backend';
import { useNavigate } from 'react-router';
import './register.css'

const Register = () => {
    const [user, setUser] = useState({Name:'', Lastname:'', PhoneNumber:0, Email:'', Password:''});
    const navigate = useNavigate();

    const registerUser = async () => {
        try {
            await backend.post('/account/register', user);
            window.confirm("User successfully registred");
            navigate('/');   
        } catch (error) {
            alert(error);
        }
    }

    const registerSubmit = (e) => {
        e.preventDefault();

        registerUser();     
    }

    return (
        <div className="login">
            <h3 className="registerHeader">Create your account</h3>

            <form className="loginForm" onSubmit={(e) => registerSubmit(e)}>
                <label>
                    <i className="fa-solid fa-user" />
                    <input type="text" placeholder='Name' onChange={(e) => setUser(prevState => ({...prevState, Name:e.target.value}))}/>
                </label>
                <label>
                    <i className="fa-solid fa-user" />
                    <input type="text" placeholder='Lastname' onChange={(e) => setUser(prevState => ({...prevState, Lastname:e.target.value}))}/>
                </label>
                <label>
                    <i className="fa-solid fa-at" />
                    <input type="email" placeholder='Email' onChange={(e) => setUser(prevState => ({...prevState, Email:e.target.value}))}/>
                </label>
                <label>
                    <i className="fa-solid fa-phone" />
                    <input type="number" placeholder='Phone Number' onChange={(e) => setUser(prevState => ({...prevState, PhoneNumber:Number(e.target.value)}))}/>
                </label>
                <label>
                    <i className="fa-solid fa-lock" />
                    <input type="password" placeholder='Password' onChange={(e) => setUser(prevState => ({...prevState, Password:e.target.value}))}/>
                </label>
                <button className="loginButton" type='submit'>
                    Register
                </button>
            </form>

            <div className="signUpAddition">
                <a href='/' className='link'>Go Back</a>
            </div>
        </div>
    )
}

export default Register