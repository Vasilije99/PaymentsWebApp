import React, { useState } from 'react'
import { Elements, CardElement, ElementsConsumer } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';
import './creditCard.css'

const stripePromise = loadStripe('pk_test_51K1AwBDCC8Upr6zGBGJgTROKSrHAsTlOWfiW0C1JOwgfippNLjk9ySMl6rBr394h0YZaNtpm5KBB5phA6yEfToCS00ILny4O0w');

const CreditCard = ({ btnText, transaction, desc }) => {
    const [amount, setAmount] = useState(0);

    const handleSubmit = async (e, elements, stripe) => {
        e.preventDefault();
        const cardElement = elements.getElement(CardElement);
        const { error } = await stripe.createPaymentMethod({ type: 'card', card: cardElement });

        if (error) {
            alert(error.message)
        } else {
            transaction(amount);
        }
    }

    return (
        <Elements stripe={stripePromise}>
            <ElementsConsumer>
                {({ elements, stripe }) => (
                    <form onSubmit={(e) => handleSubmit(e, elements, stripe)} className='ccForm'>
                        <input type="text" placeholder='Amount ($)' onChange={(e) => setAmount(parseFloat(e.target.value))} />
                        <input type="text" placeholder='Name' required />
                        <p>{desc}</p>
                        <CardElement required />
                        <br /> 
                        <button type='submit'>
                            {btnText}
                        </button>
                    </form>
                )}
            </ElementsConsumer>
        </Elements>
    )
}

export default CreditCard