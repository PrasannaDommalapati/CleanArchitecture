import pic1 from "../../assets/pic1.svg";

const Home = () => {
  return (
    <div className="main">
      <div className="main__container">
        <div className="main__content">
          <h1>SYNC WORK</h1>
          <h2>TECHNOLOGY</h2>
          <p>See what makes up difference to employee and employer.</p>
          <button className="main__btn"><a href="/signup">Get Started</a></button>
        </div>
        <div className="main__img--container">
          <img id="main__img" src={pic1} />
        </div>
      </div>
    </div>
  )
}

export default Home