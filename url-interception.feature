Feature: URL Interception

  @WBPM-1 @MANUAL
  Scenario: URL Selected By User
    Given that the app is the Default Browser on Windows
    When a user selects a URL in a non-browser app
    Then the application will show a popup with Edge and Chrome
