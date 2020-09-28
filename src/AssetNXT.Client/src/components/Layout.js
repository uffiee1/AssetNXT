import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { MapNXT } from './Map';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div className="container-fluid">
        <div className="row vh-100">

          <div className="d-flex flex-column h-100
               col-12 col-sm-5 col-lg-3 col-xl-2">

            { /* AssetNXT Logo */ }
            <div className="row d-flex flex-row
                 align-items-center bg-white" style={{ height: '64px', minHeight: '64px' }}>

              <div className="flex-grow-1 p-1 h-100">
              <img className="h-100 img-fluid" src="images/logo.png" /></div>

              <label for="toggle" className="m-0 mr-3">
                <i className="fa-lg fas fa-chevron-left" style={{ cursor: "pointer" }}></i>
                <i className="fa-lg fas fa-chevron-left" style={{ cursor: "pointer" }}></i>
              </label>

            </div>

            { /* AssetNXT Asset List */ }
            <div className="row d-flex flex-column flex-grow-1 h-100
                 align-items-center overflow-hidden bg-white p-2">

              { /* AssetNXT Asset Item */}
              <div className="container-fluid p-2 bg-white border">
                <div className="d-flex flex-row align-items-center">

                  <div className="position-relative p-2 mr-3">
                    <i style={{ color: "#3F3F3F" }} className="fas fa-2x fas fa-truck"></i>
                  </div>

                  <div className="d-flex flex-column flex-grow-1 overflow-hidden">
                    <h5 style={{ color: "#3F3F3F" }} className="text-turncate m-0"> <b>Asset #001</b></h5>
                    <h6 style={{ color: "#3F3F3F" }} className="text-turncate m-0">Boschdijktunnel, Eindhoven</h6> 
                  </div>

                </div>
              </div>

            </div>

          </div>

          <div className="d-none d-sm-flex flex-column
               h-100 col-12 col-sm-7 col-lg-9 col-xl-10">

            { /* AssetNXT Navigation Bar */}
            <div className="row d-flex flex-row
                 align-items-center bg-white" style={{ height: '0px', minHeight: '0px' }}>
              { /* <h2 className="m-auto">[Navigation Bar]</h2> */ }
            </div>

            { /* AssetNXT Map */}
            <div className="row d-flex flex-column flex-grow-1 h-100
                 align-items-center overflow-hidden bg-white">
              <MapNXT />
            </div>

          </div>


        </div>
      </div>
    );
  }
}
