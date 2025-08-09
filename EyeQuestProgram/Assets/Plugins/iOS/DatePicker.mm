#import <UIKit/UIKit.h>

// ให้แน่ใจว่ามี UnitySendMessage
extern "C" void UnitySendMessage(const char* obj, const char* method, const char* msg);

// ===== Helper VC =====
@interface DatePickerHostVC : UIViewController
@property (nonatomic, strong) UIDatePicker *picker;
@property (nonatomic, strong) NSDateFormatter *fmt;
@property (nonatomic, copy)   NSString *goName;
@property (nonatomic, copy)   NSString *method;
@end

@implementation DatePickerHostVC

- (void)viewDidLoad {
    [super viewDidLoad];
    self.view.backgroundColor = [UIColor systemBackgroundColor];

    // Formatter (ส่งกลับเป็น ค.ศ.)
    self.fmt = [NSDateFormatter new];
    self.fmt.locale = [NSLocale localeWithLocaleIdentifier:@"en_US_POSIX"];
    self.fmt.calendar = [[NSCalendar alloc] initWithCalendarIdentifier:NSCalendarIdentifierGregorian];
    self.fmt.dateFormat = @"yyyy-MM-dd";

    // Toolbar
    UIToolbar *bar = [UIToolbar new];
    bar.translatesAutoresizingMaskIntoConstraints = NO;
    UIBarButtonItem *cancel = [[UIBarButtonItem alloc] initWithTitle:@"ยกเลิก"
                                                               style:UIBarButtonItemStylePlain
                                                              target:self
                                                              action:@selector(onCancel)];
    UIBarButtonItem *flex = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemFlexibleSpace
                                                                          target:nil
                                                                          action:nil];
    UIBarButtonItem *done = [[UIBarButtonItem alloc] initWithTitle:@"ตกลง"
                                                             style:UIBarButtonItemStyleDone
                                                            target:self
                                                            action:@selector(onDone)];
    [bar setItems:@[cancel, flex, done] animated:NO];

    // UIDatePicker
    self.picker = [UIDatePicker new];
    self.picker.translatesAutoresizingMaskIntoConstraints = NO;
    self.picker.datePickerMode = UIDatePickerModeDate;

    // บังคับให้ UI แสดงเป็น ค.ศ. (ไม่ใช้ พ.ศ.)
    self.picker.calendar = [[NSCalendar alloc] initWithCalendarIdentifier:NSCalendarIdentifierGregorian];
    self.picker.locale   = [NSLocale localeWithLocaleIdentifier:@"en_US_POSIX"];

    if (@available(iOS 13.4, *)) {
        self.picker.preferredDatePickerStyle = UIDatePickerStyleWheels; // เปลี่ยนเป็น Inline ได้ถ้าต้องการ
    }

    // Stack = Toolbar + Picker
    UIStackView *stack = [[UIStackView alloc] initWithArrangedSubviews:@[bar, self.picker]];
    stack.axis = UILayoutConstraintAxisVertical;
    stack.translatesAutoresizingMaskIntoConstraints = NO;
    stack.spacing = 0;
    [self.view addSubview:stack];

    UILayoutGuide *g = self.view.safeAreaLayoutGuide;
    [NSLayoutConstraint activateConstraints:@[
        [stack.leadingAnchor constraintEqualToAnchor:g.leadingAnchor],
        [stack.trailingAnchor constraintEqualToAnchor:g.trailingAnchor],
        [stack.topAnchor constraintEqualToAnchor:g.topAnchor],
        [stack.bottomAnchor constraintEqualToAnchor:g.bottomAnchor],
        [bar.heightAnchor constraintEqualToConstant:44.0],
        [self.picker.heightAnchor constraintGreaterThanOrEqualToConstant:216.0]
    ]];
}

- (void)onCancel {
    [self dismissViewControllerAnimated:YES completion:nil];
}

- (void)onDone {
    NSString *dateStr = [self.fmt stringFromDate:self.picker.date];
    UnitySendMessage(self.goName.UTF8String, self.method.UTF8String, dateStr.UTF8String);
    [self dismissViewControllerAnimated:YES completion:nil];
}
@end

// ===== Public C function for Unity =====
extern "C" void _ShowDatePicker(const char* gameObjectName, const char* callbackMethod)
{
    NSString *goName = [NSString stringWithUTF8String:gameObjectName ?: ""];
    NSString *method = [NSString stringWithUTF8String:callbackMethod ?: ""];

    dispatch_async(dispatch_get_main_queue(), ^{
        // หา top-most VC
        UIViewController *root = nil;
        if (@available(iOS 13.0, *)) {
            for (UIWindowScene *scene in UIApplication.sharedApplication.connectedScenes) {
                if (scene.activationState == UISceneActivationStateForegroundActive) {
                    for (UIWindow *w in scene.windows) {
                        if (w.isKeyWindow) { root = w.rootViewController; break; }
                    }
                }
                if (root) break;
            }
        } else {
            root = UIApplication.sharedApplication.keyWindow.rootViewController;
        }
        while (root.presentedViewController) {
            root = root.presentedViewController;
        }

        DatePickerHostVC *vc = [DatePickerHostVC new];
        vc.goName = goName;
        vc.method = method;

        // แสดงแบบ sheet (ไม่ทับ wheel)
        if (@available(iOS 13.0, *)) {
            vc.modalPresentationStyle = UIModalPresentationPageSheet;
        } else {
            vc.modalPresentationStyle = UIModalPresentationFormSheet;
        }

        // iOS 15+ ให้ปรับขนาด sheet ได้
        if (@available(iOS 15.0, *)) {
            UISheetPresentationController *sheet = vc.sheetPresentationController;
            if (sheet) {
                sheet.detents = @[UISheetPresentationControllerDetent.mediumDetent,
                                  UISheetPresentationControllerDetent.largeDetent];
                sheet.prefersGrabberVisible = YES;
                sheet.preferredCornerRadius = 20.0;
            }
        }

        [root presentViewController:vc animated:YES completion:nil];
    });
}
