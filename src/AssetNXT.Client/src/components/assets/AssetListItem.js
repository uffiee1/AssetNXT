import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';

import './Asset.css'
import './AssetListItem.css'
import { Asset } from './Asset';

export class AssetListItem extends Component {

  render() {
    return (

      <Container className="asset-item-container">
        <Row className="asset-item-row">
          <Col className="asset-item-col">

            <Asset name={this.props.name}
             description={this.props.description} />

          </Col>
        </Row>
      </Container>

    );
  }
}