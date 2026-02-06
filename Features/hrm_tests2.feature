
@Site:OrangeHRM @User:Admin
Feature: Test OrangeHRM2

@Site:OrangeHRM @User:Admin
Scenario: Admin can access dashboard on Site A
  Then the dashboard should be visible

##@Authenticated @SiteA @Admin
#Scenario: Admin view the PIM page
#  When click on PIM menu
#
##@Authenticated @SiteA @Admin
#Scenario: Admin view the Admin page
#  When click on Admin menu
#  Then verify admin page

