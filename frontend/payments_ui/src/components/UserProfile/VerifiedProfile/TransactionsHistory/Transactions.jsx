import React, {useState, useEffect} from 'react'
import backend from '../../../../api/backend'
import { useNavigate } from 'react-router'
import './transactions.css'

const Transactions = () => {
    const [transactions, setTransactions] = useState([]);
    const id =  window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1);
    const navigate = useNavigate();

    const fetchTransactions = async () => {
        const response = await backend.get(`/transactions/get/${id}`);
        setTransactions(response.data);
    }
    
    useEffect(() => {
        fetchTransactions();
        console.log(transactions);
    }, []);

    const renderedContent = transactions.map((item) => {
        return(
            <tr key={item.id} className="transaction">
                <td>{item.typeString}</td>
                <td>{item.date}</td>
                <td>{item.amount}$</td>
            </tr>
        )
    })

    return (
        <div className='transactionsDiv'>
            <table className="transactions">
                <thead>
                    <tr>
                        <th>Transaction Type</th>
                        <th>Transaction Date</th>
                        <th>Amount</th>
                    </tr>
                </thead>
                <tbody>
                    {renderedContent}
                </tbody>
            </table>
            <button className='verifyButton' onClick={() => navigate(`/profile/${id}`)}>Go Back </button>
        </div>
    )
}

export default Transactions