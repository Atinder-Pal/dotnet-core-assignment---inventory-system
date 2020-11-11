import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { GetFullInventory } from './components/GetFullInventory';
import { GetActiveInventory } from './components/GetActiveInventory';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/get-inventory' component={GetFullInventory} />
        <Route path='/get-active-inventory' component={GetActiveInventory} />
      </Layout>
    );
  }
}
