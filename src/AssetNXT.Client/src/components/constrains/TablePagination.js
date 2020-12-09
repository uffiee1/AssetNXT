import React, { Component } from "react";
import { Row, Col, Pagination, PaginationItem, PaginationLink } from "reactstrap";
export default class TablePagination extends Component {

    setPageIndex(i) {
        if (i < this.props.max && i >= this.props.min) {
            this.props.setIndex(i)
        }
    }

    returnIndexButtons() {
        return (
        [...Array(this.props.max)].map((e, i) => {
            return (
                <PaginationItem key={i}>
                    <PaginationLink onClick={() => this.setPageIndex(i)}>
                        {i + 1}
                    </PaginationLink>
                </PaginationItem>
            )
        }))
    }

    render() {
        return (
            <Row >
                <Col className="d-flex justify-content-center align-items-center">
                    <Pagination>
                        <PaginationItem>
                            <PaginationLink previous onClick={() => this.setPageIndex(this.props.index - 1)} />
                        </PaginationItem>
                        {this.returnIndexButtons()}        
                        <PaginationItem>
                            <PaginationLink next onClick={() => this.setPageIndex(this.props.index + 1)} />
                        </PaginationItem>
                    </Pagination>
                </Col>
            </Row>
            );
    }

}