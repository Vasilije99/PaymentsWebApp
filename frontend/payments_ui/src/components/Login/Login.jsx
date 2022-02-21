import React, {useState} from 'react'
import backend from '../../api/backend'
import { useNavigate } from 'react-router'
import './login.css'

const Login = () => {
    const [user, setUser] = useState({Email:'', Password:''});
    const navigate = useNavigate();

    const login = async () => {
        try{
            const response = await backend.post('/account/login', user);
            window.confirm("User is successfully logged-in");
            navigate(`/profile/${response.data.id}`);
        } catch(error) {
            alert(error);
        }
    }

    const loginSubmit = (e) => {
        e.preventDefault();

        login();
    }

    return (
        <div className="login">
            <h3 className="loginHeader">Log-in to your account</h3>

            <form className="loginForm" onSubmit={(e) => loginSubmit(e)}>
                <label>
                    <i className="fa-solid fa-user"/>
                    <input type="email" placeholder='Email address' onChange={(e) => setUser(prevState => ({...prevState, Email:e.target.value}))}/>
                </label>
                <label>
                    <i className="fa-solid fa-lock"/>
                    <input type="password" placeholder='Password' onChange={(e) => setUser(prevState => ({...prevState, Password:e.target.value}))}/>
                </label>
                <button className="loginButton" type='submit'>
                    Login
                </button>
            </form>

            <div className="signUpAddition">
                <a href='/register' className='link'>Sign Up</a>
            </div>
        </div>
    )
}

export default Login