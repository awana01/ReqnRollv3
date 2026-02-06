
@Authenticated @Site:OrangeHRM @User:Admin
Feature: Test OrangeHRM1


Scenario: Admin can access dashboard on Site A
  Then the dashboard should be visible


Scenario: Admin view the PIM page
  When click on PIM menu


Scenario: Admin view the Admin page
  When click on Admin menu
  Then verify admin page


Scenario: Admin view the Leave page
  When click on Leave menu
  #Then verify admin page


Scenario: Admin view the Time page
  When click on Time menu