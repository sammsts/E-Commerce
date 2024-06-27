import '../../components/formloginregister/style.css';
import axios from 'axios';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const FormLoginRegister = () => {
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();
    sessionStorage.setItem('Authenticated', false);

    const handleLogin = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7063/api/Usuario/Login', {
                email: email,
                password: password
            });

            if (response.status == 200) {
                const token = response.data.token;
                localStorage.setItem('tokenJWT', token);
                sessionStorage.setItem('Authenticated', true);
                sessionStorage.setItem('UserName', response.data.userName);
                sessionStorage.setItem('UserEmail', response.data.userEmail);
                sessionStorage.setItem('UserImgProfile', response.data.userImgProfile);
            }

            accessSystem();
        } catch (error) {
            setError(error.response.data);
        }
    };

    const handleSignUp = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7063/api/Usuario/RegistrarUsuario', {
                Usu_nome: nome,
                Usu_email: email,
                Usu_senha: password
            });

            if (response.status == 200) {
                console.log("CADASTRADO");
            }

            accessSystem();
        } catch (error) {
            setError(error.response.data);
        }
    };

    const accessSystem = async () => {
        if (sessionStorage.getItem('Authenticated')) {
            navigate('/home');
        }
    };

    return (
        <>
            <div className="tooltip-container">
                <div className="tooltip">
                    <div className="profile">
                        <div className="user">
                            <div className="img">$</div>
                            <div className="details">
                                <div className="name">Ecommerce</div>
                                <div className="username">@ecommerce</div>
                            </div>
                        </div>
                        <div className="about">500+ Connections</div>
                    </div>
                </div>
                <div className="text">
                    <a className="icon" href="/">
                        <div className="layer">
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                            <svg viewBox="0 0 1024 1024" enableBackground="new 0 0 1024 1024" id="shop" version="1.1" xmlSpace="preserve" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" fill="#000000"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <g> <g id="shop-label"> <rect fill="#50bcf2" height="1024" width="1024"></rect> </g> <g id="shop-shop"> <g> <g> <polygon fill="#FFFFFF" points="276.48,778.7598 256,778.7598 256,551.8604 276.48,551.8604 276.48,778.7598 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="768,778.7598 747.5195,778.7598 747.5195,551.8604 768,551.8604 768,778.7598 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="573.4399,791.2402 552.96,791.2402 552.96,618.1406 471.04,618.1406 471.04,791.2402 450.5601,791.2402 450.5601,597.6602 573.4399,597.6602 573.4399,791.2402 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="542.6699,704.9805 522.1899,704.9805 522.1899,685.5 542.6699,685.5 542.6699,704.9805 "></polygon> </g> <g> <g> <polygon fill="#FFFFFF" points="783.3594,511.0996 762.8799,511.0996 762.8799,388.9004 261.1201,388.9004 261.1201,511.0996 240.6396,511.0996 240.6396,368.4199 783.3594,368.4199 783.3594,511.0996 "></polygon> </g> <g> <path d="M732.9004,572.5996c-18.7256,0-35.291-11.0391-44.0156-27.4795 c-8.7344,16.4404-25.295,27.4795-43.9952,27.4795c-18.7148,0-35.2695-11.0596-43.9951-27.5 c-8.7197,16.4404-25.2695,27.5-43.9745,27.5c-18.7197,0-35.2749-11.0391-43.9951-27.4795 c-8.7246,16.4404-25.2695,27.4795-43.9648,27.4795c-18.7198,0-35.2803-11.0391-44.0098-27.4795 c-8.7305,16.4404-25.2852,27.4795-43.9805,27.4795c-18.7197,0-35.2744-11.0391-43.9951-27.4795 c-8.7246,16.4404-25.2847,27.4795-43.9946,27.4795c-27.0699,0-50.5401-22.5293-52.3198-50.2197l-0.0206-21.5195h542.7198 l-0.0791,20.8994C781.4502,550.54,759.3301,572.5996,732.9004,572.5996L732.9004,572.5996z M702.9395,521.3398 c1.2402,17.2803,14.3505,30.7803,29.9609,30.7803c15.5742,0,28.6601-13.5,29.8994-30.7803H702.9395L702.9395,521.3398z M614.96,521.3398c1.2451,17.2803,14.3447,30.7803,29.9296,30.7803c15.585,0,28.6954-13.5,29.96-30.7803H614.96 L614.96,521.3398z M526.9902,521.3398c1.2246,17.2803,14.3296,30.7803,29.9297,30.7803c15.5752,0,28.6651-13.5,29.9102-30.7803 H526.9902L526.9902,521.3398z M439,521.3398c1.2402,17.2803,14.3496,30.7803,29.96,30.7803 c15.5703,0,28.6601-13.5,29.9199-30.7803H439L439,521.3398z M351.04,521.3398c1.2246,17.2803,14.3301,30.7803,29.9297,30.7803 c15.5801,0,28.6802-13.5,29.9405-30.7803H351.04L351.04,521.3398z M261.1201,521.3398v0.3799 c1.0596,16.1807,15.6602,30.4004,31.8599,30.4004c15.5952,0,28.7051-13.5,29.9497-30.7803H261.1201L261.1201,521.3398z" fill="#FFFFFF"></path> </g> <g> <polygon fill="#FFFFFF" points="345.3599,521.2803 324.8799,521.2803 324.8799,378.6602 345.3599,378.6602 345.3599,521.2803 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="434.02,521.2803 413.54,521.2803 413.54,378.6602 434.02,378.6602 434.02,521.2803 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="611.3398,521.2803 590.8594,521.2803 590.8594,378.6602 611.3398,378.6602 611.3398,521.2803 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="699.9902,521.2803 679.5098,521.2803 679.5098,378.6602 699.9902,378.6602 699.9902,521.2803 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="522.2402,521.2803 501.7598,521.2803 501.7598,378.6602 522.2402,378.6602 522.2402,521.2803 "></polygon> </g> </g> <g> <polygon fill="#FFFFFF" points="778.2402,793.1201 245.7598,793.1201 245.7598,772.6406 778.2402,772.6406 778.2402,793.1201 "></polygon> </g> <g> <path d="M732.1602,743.0195H599.04V599.4805h133.1202V743.0195L732.1602,743.0195z M619.5195,722.54h92.1602 V619.96h-92.1602V722.54L619.5195,722.54z" fill="#FFFFFF"></path> </g> <g> <path d="M424.96,743.5801H291.8398V598.9004H424.96V743.5801L424.96,743.5801z M312.3203,723.0996H404.48 V619.3799h-92.1597V723.0996L312.3203,723.0996z" fill="#FFFFFF"></path> </g> </g> <g> <g> <polygon fill="#FFFFFF" points="434.02,383.6602 413.54,383.6602 413.54,343.5601 434.02,343.5601 434.02,383.6602 "></polygon> </g> <g> <polygon fill="#FFFFFF" points="611.3398,396.7002 590.8594,396.7002 590.8594,343.5601 611.3398,343.5601 611.3398,396.7002 "></polygon> </g> <g> <path d="M655.3701,347.4199h-286.75c-5.6553,0-10.2402-4.5801-10.2402-10.2397v-96.0601 c0-5.6601,4.5849-10.2402,10.2402-10.2402h286.75c5.6553,0,10.2393,4.5801,10.2393,10.2402v96.0601 C665.6094,342.8398,661.0254,347.4199,655.3701,347.4199L655.3701,347.4199z M378.8599,326.9399h266.27v-75.5795h-266.27 V326.9399L378.8599,326.9399z" fill="#FFFFFF"></path> </g> </g> <g> <polygon fill="#FFFFFF" points="620.7998,297.48 403.2002,297.48 403.2002,277 620.7998,277 620.7998,297.48 "></polygon> </g> </g> </g> </g></svg>
                        </div>
                        <div className="text">E-commerce</div>
                    </a>
                </div>
            </div>
            <div className="wrapperLG">
                <div className="card-switch">
                    <label className="switchLG">
                        <input type="checkbox" className="toggleLG" />
                        <span className="sliderLG"></span>
                        <span className="card-sideLG"></span>
                        <div className="flip-card__innerLG">
                            <div className="flip-card__frontLG">
                                <div className="titleLG">Log in</div>
                                <form className="flip-card__formLG" onSubmit={ handleLogin }>
                                    <input className="flip-card__inputLG" name="email" placeholder="Email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                                    <input className="flip-card__inputLG" name="password" placeholder="Password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                                    <button className="flip-card__btnLG" type="submit">Let`s go!</button>
                                    <label className="text-red-600 underline decoration-wavy underline-offset-2 normal-case font-semibold">{error}</label>
                                </form>
                            </div>
                            <div className="flip-card__backLG">
                                <div className="titleLG">Sign up</div>
                                <form className="flip-card__formLG" onSubmit={ handleSignUp }>
                                    <input className="flip-card__inputLG" placeholder="Name" type="name" value={nome} onChange={(e) => setNome(e.target.value)} />
                                    <input className="flip-card__inputLG" name="email" placeholder="Email" type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                                    <input className="flip-card__inputLG" name="password" placeholder="Password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                                    <button className="flip-card__btnLG" type="submit">Confirm!</button>
                                    <label className="text-red-600 underline decoration-wavy underline-offset-2 normal-case font-semibold">{error}</label>
                                </form>
                            </div>
                        </div>
                    </label>
                </div>
            </div>
        </>
    );
};

export default FormLoginRegister;