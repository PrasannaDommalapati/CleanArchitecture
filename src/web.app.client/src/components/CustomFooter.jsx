const CustomFooter = () => {
  return (
    <div className="footer__container">
      <div className="footer__links">
        <div className="footer__link--wrapper">
          <div className="footer__link--items">
            <h2>About Us</h2>
            <a href="/sign__up">How it works</a> <a href="/">Testimonials</a>
            <a href="/">Careers</a> <a href="/">Investments</a>
            <a href="/">Terms of Service</a>
          </div>
          <div className="footer__link--items">
            <h2>Contact Us</h2>
            <a href="/">Contact</a> <a href="/">Support</a>
            <a href="/">Destinations</a> <a href="/">Sponsorships</a>
          </div>
        </div>
        <div className="footer__link--wrapper">
          <div className="footer__link--items">
            <h2>Videos</h2>
            <a href="/">Submit Video</a> <a href="/">Ambassadors</a>
            <a href="/">Agency</a> <a href="/">Influencer</a>
          </div>
          <div className="footer__link--items">
            <h2>Social Media</h2>
            <a href="/">Instagram</a> <a href="/">Facebook</a>
            <a href="/">Youtube</a> <a href="/">Twitter</a>
          </div>
        </div>
      </div>
      <section className="social__media">
        <div className="social__media--wrap">
          <div className="footer__logo">
            <a href="/" id="footer__logo">
              <i className="fas fa-gem"></i>NEXT
            </a>
          </div>
          <p className="website__rights">© NEXT 2020. All rights reserved</p>
          <div className="social__icons">
            <a
              className="social__icon--link"
              href="/"
              target="_blank"
              aria-label="Facebook"
            >
              <i className="fab fa-facebook"></i>
            </a>
            <a
              className="social__icon--link"
              href="/"
              target="_blank"
              aria-label="Instagram"
            >
              <i className="fab fa-instagram"></i>
            </a>
            <a
              className="social__icon--link"
              href="//www.youtube.com/channel/UCsKsymTY_4BYR-wytLjex7A?view_as=subscriber"
              target="_blank"
              aria-label="Youtube"
            >
              <i className="fab fa-youtube"></i>
            </a>
            <a
              className="social__icon--link"
              href="/"
              target="_blank"
              aria-label="Twitter"
            >
              <i className="fab fa-twitter"></i>
            </a>
            <a
              className="social__icon--link"
              href="/"
              target="_blank"
              aria-label="LinkedIn"
            >
              <i className="fab fa-linkedin"></i>
            </a>
          </div>
        </div>
      </section>
    </div>
  );
};

export default CustomFooter;
