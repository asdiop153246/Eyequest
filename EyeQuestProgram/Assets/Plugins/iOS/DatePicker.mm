#import <UIKit/UIKit.h>

extern "C" {
    void ShowDatePicker(const char* gameObjectName, const char* callbackMethod) {
        NSString* goName = [NSString stringWithUTF8String:gameObjectName];
        NSString* cbMethod = [NSString stringWithUTF8String:callbackMethod];

        dispatch_async(dispatch_get_main_queue(), ^{
            UIDatePicker* picker = [[UIDatePicker alloc] init];
            picker.datePickerMode = UIDatePickerModeDate;

            UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"Select Date"
                                                                           message:nil
                                                                    preferredStyle:UIAlertControllerStyleActionSheet];

            [alert.view addSubview:picker];

            UIAlertAction *ok = [UIAlertAction actionWithTitle:@"OK"
                                                         style:UIAlertActionStyleDefault
                                                       handler:^(UIAlertAction * action) {
                NSDateFormatter *formatter = [[NSDateFormatter alloc] init];
                [formatter setDateFormat:@"yyyy-MM-dd"];
                NSString *dateString = [formatter stringFromDate:picker.date];

                UnitySendMessage([goName UTF8String], [cbMethod UTF8String], [dateString UTF8String]);
            }];

            [alert addAction:ok];
            [[UIApplication sharedApplication].keyWindow.rootViewController presentViewController:alert animated:YES completion:nil];
        });
    }
}
