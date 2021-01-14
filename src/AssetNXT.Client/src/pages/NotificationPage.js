import React, { Component } from "react";
import NotificationTable from "../components/notification/NotificationTable";
import Layout from "../components/Layout";

export default class NotificationPage extends Component {
  render() {
    return <Layout dock={<NotificationTable /> } />;
  }
}
