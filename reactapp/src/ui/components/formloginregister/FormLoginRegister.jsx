import '../../components/formloginregister/style.css';

const FormLoginRegister = () => {
    return (
        <div className="wrapperLG">
            <div className="card-switch">
                <label className="switchLG">
                    <input type="checkbox" className="toggleLG" />
                    <span className="sliderLG"></span>
                    <span className="card-sideLG"></span>
                    <div className="flip-card__innerLG">
                        <div className="flip-card__frontLG">
                            <div className="titleLG">Log in</div>
                            <form className="flip-card__formLG" action="">
                                <input className="flip-card__inputLG" name="email" placeholder="Email" type="email" />
                                <input className="flip-card__inputLG" name="password" placeholder="Password" type="password" />
                                <button className="flip-card__btnLG">Let`s go!</button>
                            </form>
                        </div>
                        <div className="flip-card__backLG">
                            <div className="titleLG">Sign up</div>
                            <form className="flip-card__formLG" action="">
                                <input className="flip-card__inputLG" placeholder="Name" type="name" />
                                <input className="flip-card__inputLG" name="email" placeholder="Email" type="email" />
                                <input className="flip-card__inputLG" name="password" placeholder="Password" type="password" />
                                <button className="flip-card__btnLG">Confirm!</button>
                            </form>
                        </div>
                    </div>
                </label>
            </div>
        </div>
    );
};

export default FormLoginRegister;