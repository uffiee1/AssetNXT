import React, { useState }from 'react';
import { Container, Col, Row } from 'reactstrap';
import DateFnsUtils from '@date-io/date-fns';
//import { format } from 'date-fns'
import { KeyboardDateTimePicker, MuiPickersUtilsProvider } from '@material-ui/pickers';

const DatePicker = (props) => {

    const [selectedFromDate, handleFromDateChange] = useState(null);
    const [selectedToDate, handleToDateChange] = useState(null);
    return (
        <Container fluid className="mb-5">
            <MuiPickersUtilsProvider utils={DateFnsUtils}>
                <Row>
                    <Col xs="auto">
                        <KeyboardDateTimePicker
                            label="From"
                            inputVariant="outlined"
                            showTodayButton={true}

                            disableFuture
                            format="yyyy/MM/dd HH:mm"
                            onError={console.log}

                            name="selectedFromDate"
                            value={selectedFromDate}
                            onChange={handleFromDateChange}
                        />
                    </Col>
                    <Col xs="auto">
                        <KeyboardDateTimePicker
                            label="To"
                            inputVariant="outlined"
                            showTodayButton={true}

                            disablePast
                            format="yyyy/MM/dd HH:mm"
                            onError={console.log}

                            name="selectedToDate"
                            value={selectedToDate}
                            onChange={handleToDateChange}
                        />
                    </Col>
                </Row>
            </MuiPickersUtilsProvider>
        </Container>
    )
}

export default DatePicker;