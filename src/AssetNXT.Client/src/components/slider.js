import React from "react";
import Nouislider from "nouislider-react";
import "nouislider/distribute/nouislider.css";

class Slider extends React.Component {
    state = {
        textValue: null,
        percent: null
    };

    onSlide = (render, handle, value, un, percent) => {
        this.setState({
            textValue: value[0].toFixed(2),
            percent: percent[0].toFixed(2)
        });
    };

    render() {
        const { textValue, percent } = this.state;
        return (
            <div>
                <Nouislider
                    connect
                    accessibility
                    start={[25, 50]}
                    step={1}
                    range={{
                        min: 0,
                        max: 100
                    }}
                    onSlide={this.onSlide}
                />
                {textValue && percent && (
                    <div>
                        Value: {textValue}, {percent} %
                    </div>
                )}
            </div>
        );
    }
}

export default Slider;