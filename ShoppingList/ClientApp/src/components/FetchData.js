import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = 'New name';

  constructor(props) {
    super(props);
    this.state = { shoppingLists: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderShoppingListsTable(shoppingLists) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Category</th>
            <th>Name</th>
            <th>Description</th>
            <th>IsCompleted</th>
          </tr>
        </thead>
        <tbody>
            {shoppingLists.map(shoppingList =>
                <tr key={shoppingList.category}>
                <td>{shoppingList.category}</td>
                <td>{shoppingList.name}</td>
                <td>{shoppingList.description}</td>
                <td>{shoppingList.isCompleted ? "True" : "False"}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderShoppingListsTable(this.state.shoppingLists);

    return (
      <div>
        <h1 id="tabelLabel" >Shopping Lists</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('shoppinglists');
    const data = await response.json();
    this.setState({ shoppingLists: data, loading: false });
  }
}
