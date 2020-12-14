import React, { Component } from "react";

export default class NotificationConfiguration extends Component {
   constructor(props) {
    super(props)
    this.state = {
      id: '',
      title: '',
      description: ''
    };
    this.baseState = this.state;
  }

  inputChange = (event) => {
    event.preventDefault()
    this.setState({
      [event.target.name]: event.target.value
    })
  }

  async postData(e) {
    e.preventDefault();
    //console.log(JSON.stringify({
    //    deviceId: 'test1',
    //    points: this.state.points
    //}));
    const response = await fetch('api/notifications', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        deviceId: 'test',
        title: this.state.title,
        description: this.state.description
      })
    });
    this.setState(this.baseState);
    //return response.json();
  }

  async updateData(e) {
    e.preventDefault();
    const response = await fetch('api/notifications/', {
      method: 'PUT',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        deviceId: this.state.id,
        title: this.state.title,
        description: this.state.description
      })
    });
    this.setState(this.baseState);
    return response.json();
  }

  async deleteData(e) {
    e.preventDefault();
    await fetch('api/notifications/' + this.state.id, {
      method: 'DELETE'
    }).then(response => {
      if(response.ok)
      {
        alert('deleted!')
      }
      else
      {
        alert('error!, not deleted!!!')
      }
    });
    this.setState(this.baseState);
  }


  render() {
    return (
      <div>
        <form onSubmit={(e) => this.postData(e)}>
          <label>Title: </label><input type='text' name='title' value={this.state.title} onChange={this.inputChange} /><br/>
          <label>Description: </label><input type='text' name='description' value={this.state.description} onChange={this.inputChange}/><br/>
          <input type='submit' value='Create' />
        </form>
        <form onSubmit={(e => this.updateData(e))}>
          <label></label>
          <input type='submit' value='Update' />
        </form>
        <form onSubmit={(e) => this.deleteData(e)}>
          <label>Id: </label><input type='text' name='id' value={this.state.id} onChange={this.inputChange} /><br/>
          <input type='submit' value='Delete' />
        </form>
      </div>
    )
  }
}

