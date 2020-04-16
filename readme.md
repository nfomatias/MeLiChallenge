# Install instruction
docker build -t melichallenge .

# Run command
docker-compose up

# Usage modes

## Api calls with ip address

### IP BR 
curl http://localhost:8080/api/ipinfo/179.183.250.219 -UseBasicParsing

### IP ES 
curl http://localhost:8080/api/ipinfo/213.192.202.121 -UseBasicParsing

### IP UY 
curl http://localhost:8080/api/ipinfo/200.40.96.160 -UseBasicParsing

### IP USA 
curl http://localhost:8080/api/ipinfo/72.229.28.185 -UseBasicParsing

## Statistic api call
curl http://localhost:8080/api/ipinfo/ -UseBasicParsing
