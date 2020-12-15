import React from "react";
import { Container, Col, Row } from "reactstrap";
import moment from "moment";
import MomentUtils from "@date-io/moment";
import {
  KeyboardDateTimePicker,
  MuiPickersUtilsProvider,
} from "@material-ui/pickers";

const DatePicker = (props) => {
  return (
    <Container fluid className="mt-5">
      <MuiPickersUtilsProvider libInstance={moment} utils={MomentUtils}>
        <Row>
          <Col xs="auto">
            <KeyboardDateTimePicker
              label="From"
              inputVariant="outlined"
              showTodayButton={true}
              disableFuture
              maxDate={props.maxDate || moment.utc().toISOString()}
              format="yyyy/MM/dd HH:mm"
              onError={console.log}
              name="selectedFromDate"
              value={props.minDate}
              onChange={(date) => props.minDateChanged(date)}
            />
          </Col>
          <Col xs="auto">
            <KeyboardDateTimePicker
              label="To"
              inputVariant="outlined"
              showTodayButton={true}
              disableFuture
              minDate={props.minDate || moment.utc().toISOString()}
              format="yyyy/MM/dd HH:mm"
              onError={console.log}
              name="selectedToDate"
              value={props.maxDate}
              onChange={(date) => props.maxDateChanged(date)}
            />
          </Col>
        </Row>
      </MuiPickersUtilsProvider>
    </Container>
  );
};

export default DatePicker;
