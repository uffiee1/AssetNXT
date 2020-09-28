import React from 'react';

export default class Tooltip extends React.Component {
    state = {
        loading: true,
        tag: null
    };

    async componentDidMount() {
        const url = "https://ruuvi-api.herokuapp.com/";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ tag: data.tags, loading: false });
        console.log("method called");
    }

    round(numb) {
        return Math.round(numb * 100) / 100
    }
    render() {
        const divWidth = { width: "200px" };
        if (this.state.loading) {
            return <div style={divWidth}>loading...</div>;
        }

        if (!this.state.tag) {
            return <div style={divWidth}>didn't fetch data</div>;
        }

        return (
            <div style={divWidth} className="row m-0">
                <div className="col-3"><i className="fa fa-truck fa-3x"></i></div>
                <div className="col-9 d-flex"><span className="mb-0 h4 font-weight-bold align-self-center">#Asset 1</span></div>
                <ul className="list-group list-group-flush py-3 h6 text-nowrap">
                    <li className="list-group-item">Temperature: {this.round(this.state.tag.temperature)}</li>
                    <li className="list-group-item">Humidity: {this.state.tag.humidity}</li>
                    <li className="list-group-item">Pressure: {this.round(this.state.tag.pressure)}</li>
                </ul>
                <div className="col-6 d-flex justify-content-center"><i className="fa fa-warning text-warning h5"></i></div>
                <div className="col-6 d-flex justify-content-start px-0"><a href="javascript:void(0)" className="text-primary h6">More Info</a></div>
               
            </div>
        );
    }
}
