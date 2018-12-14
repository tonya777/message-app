import React, { Component } from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import Select from 'react-select';
import Alert from 'react-s-alert';

import 'react-s-alert/dist/s-alert-default.css';
import 'react-s-alert/dist/s-alert-css-effects/stackslide.css';

class MessageComponent extends Component {
    state = {
        visible: false,
        messageId: undefined,
        messageSubject: undefined,
        messageText: undefined,
        recipients: [],
        selectedRecipients: []
    };

    onRecieveData = (data) => {
        this.setState({
            messageId: data.messageId
        });

        data.forEach(function (elem) {
            if (elem.isSent)
                Alert.info('Message was sent, id = ' + elem.messageId + " for " + elem.recipient, {
                    position: 'top-right',
                    effect: 'stackslide',
                    timeout: 'none'
                });
            else
                Alert.error('Message was not sent, id = ' + elem.messageId + " for " + elem.recipient, {
                    position: 'top-right',
                    effect: 'stackslide',
                    timeout: 'none'
                });
        });
    }

    onRecieveUsers = (data) => {
        var recs = [];

        data.forEach(function (elem) {
            recs.push({
                id: elem.id,
                value: elem.name,
                label: elem.name + " " + elem.surname,
                surname: elem.surname
            })
        });

        this.setState({
            resipients: recs
        });
    }

    showErrorAlert = (text) => {
        Alert.error(text, {
            position: 'top-right',
            effect: 'stackslide',
            timeout: 'none'
        });
    }

    showInfoAlert = (text) => {
        Alert.info(text, {
            position: 'top-right',
            effect: 'stackslide',
            timeout: 'none'
        });
    }

    formIsValid = () => {
        if (this.state.selectedRecipients.length === 0) {
            this.showErrorAlert('You should select at least one recipient')
            return false;
        }

        if (this.state.messageText === '' || !this.state.messageText) {
            this.showErrorAlert('Enter message text please')
            return false;
        }

        return true;
    }

    onSubmit = (e) => {
        e.preventDefault();
        Alert.closeAll();

        if (!this.formIsValid())
            return;

        var recipients = [];

        this.state.selectedRecipients.forEach(function (rec) {
            recipients.push({
                id: rec.id,
                name: rec.value,
                surname: rec.surname
            })
        });

        fetch('https://localhost:44324/api/app/',
            {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify(
                    {
                        Subject: this.state.messageSubject,
                        MessageBody: this.state.messageText,
                        Recipients: recipients
                    }
                )
            })
            .then(response => response && response.json())
            .then(this.onRecieveData)            
            .catch(e => this.showErrorAlert(`Server error ${e}`));
    }

    handleSubjectChange = (e) => {
        Alert.closeAll();
        this.setState({
            messageSubject: e.target.value
        });
    }

    handleMessageChanged = (e) => {
        Alert.closeAll();
        this.setState({
            messageText: e.target.value
        });
    }

    handleRecipientsChanged = (selectedRecipients) => {
        Alert.closeAll();
        this.setState({ selectedRecipients });
    }

    componentDidMount() {
        fetch('https://localhost:44324/api/app/',
            {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "Get"
            })
            .then(response => response && response.json())
            .then(this.onRecieveUsers)
            .catch(e => this.showErrorAlert(`Server error ${e}`));
        }

    render() {
        return (
            <React.Fragment>
                <div className="form-container" onSubmit={this.onSubmit}>
                    <Form>
                        <Alert stack={true} />
                        <FormGroup>
                            <Label for="subject">Subject</Label>
                            <Input name="subject" id="subject" onChange={this.handleSubjectChange} placeholder="Enter message subject" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="recipients">Recipients</Label>
                            <Select
                                name="recipients"
                                id="recipients"
                                value={this.state.selectedRecipients}
                                options={this.state.resipients}
                                isMulti={true}
                                onChange={this.handleRecipientsChanged}
                            />
                        </FormGroup>
                        <FormGroup>
                            <Label for="exampleText">Message</Label>
                            <Input type="textarea" name="text" id="exampleText" placeholder="Enter message text" onChange={this.handleMessageChanged} />
                        </FormGroup>
                        <Button>Submit</Button>
                    </Form>
                </div>
            </React.Fragment>
        );
    }
}

export default MessageComponent;