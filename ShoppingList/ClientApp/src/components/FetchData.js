import React, { Component } from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

export class FetchData extends Component {
  static displayName = "New name";

  constructor(props) {
    super(props);
    this.state = { shoppingLists: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderShoppingListsTable(shoppingLists) {
    return shoppingLists.map((shoppingList) => (
      <Row xs={1} md={4} className="g-4">
        {Array.from({ length: 4 }).map((_, idx) => (
          <Col>
            <Card style={{ width: "18rem" }}>
              <Card.Img variant="top" src="holder.js/100px180" />
              <Card.Header>{shoppingList.category}</Card.Header>
              <Card.Body>
                <Card.Title>{shoppingList.name}</Card.Title>
                <Card.Text>{shoppingList.description}</Card.Text>
                <Button variant="primary">See more details</Button>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    ));
  }

  addList() {
    console.log("DA");
    const response = fetch("shoppingLists", {
      method: "POST",
      body: JSON.stringify({
        category: "Home",
        name: "Cleaning Supplies",
        description: "For the spring cleaning",
      }),
      headers: {
        "Content-type": "application/json; charset=UTF-8",
      },
    }).then((response) => {
      return response.json();
    });
  }

  deleteList() {
    console.log("Delete");
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      FetchData.renderShoppingListsTable(this.state.shoppingLists)
    );

    return (
      <div>
        <h1 id="tabelLabel">Shopping Lists</h1>
        <p>This component demonstrates fetching data from the server.</p>
        <button
          type="button"
          className="btn btn-primary"
          onClick={this.addList}
        >
          Add List
        </button>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch("shoppinglists");
    const data = await response.json();
    this.setState({ shoppingLists: data, loading: false });
  }
}
