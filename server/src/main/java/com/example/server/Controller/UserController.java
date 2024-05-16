package com.example.server.Controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.example.server.DTO.ResponseDto;
import com.example.server.DTO.User.SignInDto;
import com.example.server.DTO.User.SignInResponseDto;
import com.example.server.DTO.User.SignUpDto;
import com.example.server.Exceptions.AuthenticationFailException;
import com.example.server.Exceptions.CustomException;
import com.example.server.Models.User;
import com.example.server.Repository.UserRepository;
import com.example.server.Service.AuthenticationService;
import com.example.server.Service.UserService;

@RequestMapping("/users")
@RestController
public class UserController {
    @Autowired
    UserRepository userRepository;

    @Autowired
    AuthenticationService authenticationService;

    @Autowired
    UserService userService;

    @PostMapping("/auth/login")
    public SignInResponseDto signIn(@RequestBody SignInDto signInDto) throws CustomException{
        return userService.signIn(signInDto);
    }

    @PostMapping("/auth/register")
    public ResponseDto signUp(@RequestBody SignUpDto signUpDto) throws CustomException{
        return userService.signUp(signUpDto);
    }

    @GetMapping("/")
    public List<User> findAllUser() throws AuthenticationFailException{
        // authenticationService.authenticate(token);
        return userRepository.findAll();
    }

    @GetMapping("/byToken/{token}")
    public User getSingleUserByToken(@PathVariable("token") String token) {
        return authenticationService.getUser(token);
    }

    @GetMapping("/byId/{userId}")
    public User getSingleUserById(@PathVariable("userId") String userId){
        return userService.getSingleUser(userId);
    }
}
