# API routes & controllers
  ## /restaurants
    ### Get all restaurants 
    ### Get restaurants by restaurant id
    ### Post new restaurant
    ### Update existing restaurant
    ### Delete existing restaurant
    ### Get restaurants by cuisine country
          /restaurants?cuisine=Italy
  ## /bookings
    ### Get all bookings
    ### Get bookings by restaurant id
    ### Post new booking
    ### Update existing booking
    ### Delete existing booking
    ### Get bookings by restaurant id and date
          /restaurants?restaurantId=1&date=14-06-2021
    ### Get bookings by unique restaurant token
          /restaurants?token=xxxxx
  ## /timeslots
    ### Get booked time slots by restaurant and date
          /timeslots?restaurantId=1&date=14-06-2021
  ## /response
    ### Get Twilio message response based on message body and sendee mobile number
          /response?messageBody=This is your confirmation...&mobile=07999999999
 
 
