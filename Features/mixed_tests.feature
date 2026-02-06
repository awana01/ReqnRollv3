

Feature: Mixed_Tests
   A short summary of the feature

#@Site:OrangeHRM @User:Admin
#Scenario: Admin can access dashboard on Site A
#   Then the dashboard should be visible
#
#@Site:DemoQA @User:DemoQAUser
#Scenario: Login as DemoQA User
#   Then user loogged in sucessfully
#
#
#@Site:DemoQA @User:DemoQAUser
#Scenario: Checking Header Mene Books
#  Then user loogged in sucessfully
#  When clicks on "Books"
#  Then verify page title as "Books"
#  
#@Site:DemoQA @User:DemoQAUser
#Scenario: Checking Header Mene Computers
#  Then user loogged in sucessfully
#  When clicks on "Computers"
#  Then verify page title as "Computers"
#
#
#@Site:DemoQA @User:DemoQAUser
#Scenario: Checking Header Mene Apparel
#  Then user loogged in sucessfully
#  When clicks on "Apparel & Shoes"
#  Then verify page title as "Apparel & Shoes"

@FreshTest
Scenario: Sample NoAuth DuckDuckGo Tests
   Given I navigate to "https://duckduckgo.com/"

#@FreshTest 
#Scenario: Sample NoAuth SagePub Tests
#   Given user navigates to "https://www.sagepub.com/"
#   When user logged in with credentials "herris@protonmail.com" and "Test@1234#1234"
#   Then user sucessfully logged in
#
#   
#@FreshTest
#Scenario: Sample NoAuth OpenTable Tests
#   Given I opened the "https://www.opentable.com/"