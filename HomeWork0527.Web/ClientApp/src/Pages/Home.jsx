import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Home = () => {

    const [people, setPeople] = useState([]);

    useEffect(() => {
        const getPeople = async () => {
            const { data } = await axios.get('/api/people/getall');
            setPeople(data);
        }

        getPeople();
    }, []);

    const onDeleteClick = async () => {
        await axios.post('/api/people/delete');
        window.location.href = 'upload';
    }
    
    return (
         <>
            <div className='row'>
                <div className='col-md-6 offset-md-3 mt-5'>
                    <button className='btn btn-danger btn-lg w-100' onClick={onDeleteClick}>Delete All</button>
                </div>
            </div>
            <table className='table table-hover table-striped table-bordered mt-5'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                    <tbody>
                        {people.map(p => <tr key={p.id}>
                                <td>{p.id}</td>
                                <td>{p.firstName}</td>
                                <td>{p.lastName}</td>
                                <td>{p.age}</td>
                                <td>{p.address}</td>
                                <td>{p.email}</td>
                            </tr>)}
                    </tbody>
                </table>
        </>

    )
}

export default Home;